using System;
using System.Collections.Generic;
using System.Text;

namespace AGLChallenge.DTO.DTOModels
{
    public class PetDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string OwnerName { get; set; }
        public string OwnerGender { get; set; }
        public int OwnerAge { get; set; }
    }
}
