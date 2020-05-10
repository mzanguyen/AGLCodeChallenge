using AGLChallenge.DTO.DTOModels;
using AGLChallenge.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGLChallenge.Services.Services
{
    public class PetService : IPetService
    {
        private IAGLWebService _webservice;

        public PetService(IAGLWebService webService) => _webservice = webService;

        //TODO: Magic string 'cat'
        public async Task<SortedList<string, List<PetDTO>>> GetCatByOwnerGender() 
            => await GetPets(pet => pet.Type.Equals("cat", StringComparison.OrdinalIgnoreCase), pet => pet.OwnerGender, pet => pet.Name);



        #region Helpers
        /// <summary>
        /// Apply filter and ordering to pet list
        /// This method can be reused for different sorting and filtering
        /// </summary>
        public async Task<SortedList<T, List<PetDTO>>> GetPets<T>(Func<PetDTO, bool> filter, Func<PetDTO, T> groupBy, Func<PetDTO, string> orderBy)
        {
            var result = new SortedList<T, List<PetDTO>>();
            var pets = await GetPets();

            var groupedPets =  pets.Where(filter).GroupBy(groupBy);

            //Apply ordering to values list
            foreach(var item in groupedPets) {
                result.Add(item.Key, item.OrderBy(orderBy).ToList());
            }

            return result;
        }

        private async Task<IEnumerable<PetDTO>> GetPets()
        {
            var people = await _webservice.GetPeople();

            //TODO: Use Automapper to shorten the code
            return people?.Where(owner => owner?.Pets?.Count > 0).
                SelectMany(owner => owner.Pets, (owner, pet) => new PetDTO()
                {
                    Name = pet.Name,
                    Type = pet.Type,
                    OwnerAge = owner.Age,
                    OwnerGender = owner.Gender,
                    OwnerName = owner.Name
                });
        }
        #endregion
    }
}
