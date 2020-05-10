using AGLChallenge.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGLChallenge.Services.Interfaces
{
    public interface IAGLWebService
    {
        public Task<List<Person>> GetPeople();
    }
}
