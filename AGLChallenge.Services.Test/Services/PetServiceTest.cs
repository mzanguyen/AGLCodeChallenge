using AGLChallenge.DTO.DTOModels;
using AGLChallenge.Services.Interfaces;
using AGLChallenge.Services.Models;
using AGLChallenge.Services.Services;
using AGLChallenge.Services.Test.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AGLChallenge.Services.Test.Services
{
    public class PetServiceTest
    {
        [Theory]
        [MemberData(nameof(GetCatByOwnerGenderTestParams))]
        public async Task GetCatByOwnerGender_ShouldFilterAndOrder(string filePath, int count, params string[] expectedFirstResult)
        {
            var petService = CreateTestService(filePath);
            var result = await petService.GetCatByOwnerGender();

            Assert.Equal(count, result.Keys.Count);
            Assert.Equal(expectedFirstResult.ToList(), result.GetValueOrDefault("Female", new List<PetDTO>()).Select(x => x.Name)?.ToList());
        }

        [Theory]
        [MemberData(nameof(GetPetsGenericTypeParams))]
        public async Task GetPetsGenericType_ShouldWorkWithDifferentFilter(string filePath, string filter, params string[] expectedFirstResult)
        {
            var petService = CreateTestService(filePath);
            var result = await petService.GetPets(pet => pet.Type.Equals(filter, StringComparison.OrdinalIgnoreCase), pet => pet.OwnerGender, pet => pet.Name);

            Assert.Equal(expectedFirstResult.ToList(), result.GetValueOrDefault("Male", new List<PetDTO>()).Select(x => x.Name)?.ToList());
        }


        #region Helpers
        public PetService CreateTestService(string filePath)
        {
            //Load mock data
            var webService = new Mock<IAGLWebService>();
            webService.Setup(x => x.GetPeople()).Returns(Task.FromResult(LoadJsonToObject.FromFile<List<Person>>(filePath)));

            return new PetService(webService.Object);
        }
        public static IEnumerable<object[]> GetCatByOwnerGenderTestParams()
        {
            yield return new object[] { "MockData/AGLWebServicePeopleSample1.json", 2, "Garfield", "Simba", "Tabby" };
            yield return new object[] { "MockData/AGLWebServicePeopleSample2.json", 2, "Simba", "Tabby" };
            yield return new object[] { "MockData/AGLWebServicePeopleSample3.json", 2};
            yield return new object[] { "MockData/AGLWebServicePeopleSample4.json", 0 };
        }

        public static IEnumerable<object[]> GetPetsGenericTypeParams()
        {
            yield return new object[] { "MockData/AGLWebServicePeopleSample1.json", "dog", "Fido", "Sam" };
            yield return new object[] { "MockData/AGLWebServicePeopleSample2.json", "cat", "Garfield", "Jim", "Max", "Tom" };
            yield return new object[] { "MockData/AGLWebServicePeopleSample3.json", "dog", "Ali", "Allias" };
            yield return new object[] { "MockData/AGLWebServicePeopleSample4.json", "dog" };
        }
        #endregion
    }
}
