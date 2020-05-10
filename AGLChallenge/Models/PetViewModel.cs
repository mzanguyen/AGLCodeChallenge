using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AGLChallenge.Client.Models
{
    public class PetViewModel
    {
        public string Gender { get; set; }
        public List<string> PetNames { get; set; }
        public PetViewModel(string gender, IEnumerable<string> petNames)
        {
            Gender = gender;
            PetNames = petNames?.ToList();
        }
    }
}
