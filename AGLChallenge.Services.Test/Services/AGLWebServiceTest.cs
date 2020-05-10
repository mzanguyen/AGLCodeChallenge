using AGLChallenge.Common.Configuration;
using AGLChallenge.Services.Services;
using AGLChallenge.Services.Test.Helpers;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AGLChallenge.Services.Test.Services
{
    public class AGLWebServiceTest
    {
        [Theory]
        [MemberData(nameof(GetPeopleTestParams))]
        public async Task GetPeople_ShouldDeserialize(string filePath, int count, int countFemale)
        {
            var webService = CreateTestService(filePath);

            var result = await webService.GetPeople();

            Assert.Equal(count, result.Count);
            Assert.Equal(countFemale, result.Where(person => person.Gender == "Female")?.Count());

        }

        #region Helper
        public AGLWebService CreateTestService(string filePath)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            //mock httpClient
            //credit: https://thecodebuzz.com/unit-test-mock-httpclientfactory-moq-net-core/
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(LoadJsonToObject.FromFileToString(filePath))
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            var option = Options.Create(new AGLWebServiceConfig()
            {
                BaseURL = "https://www.google.com/",
                PeopleURI = ""
            });

            return new AGLWebService(client, option);
        }
        public static IEnumerable<object[]> GetPeopleTestParams()
        {
            yield return new object[] { "MockData/AGLWebServicePeopleSample1.json", 6, 3 };
            yield return new object[] { "MockData/AGLWebServicePeopleSample2.json", 5, 2 };
            yield return new object[] { "MockData/AGLWebServicePeopleSample3.json", 3, 0 };
            yield return new object[] { "MockData/AGLWebServicePeopleSample4.json", 0, 0 };
        }
        #endregion
    }
}
