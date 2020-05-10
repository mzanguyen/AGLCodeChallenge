using AGLChallenge.DTO.DTOModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGLChallenge.Services.Interfaces
{
    public interface IPetService
    {
        public Task<SortedList<string, List<PetDTO>>> GetCatByOwnerGender();
    }
}
