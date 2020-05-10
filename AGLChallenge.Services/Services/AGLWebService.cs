using AGLChallenge.Common.Configuration;
using AGLChallenge.Common.Extensions;
using AGLChallenge.Services.Interfaces;
using AGLChallenge.Services.Models;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AGLChallenge.Services.Services
{
    public class AGLWebService : BaseWebService, IAGLWebService
    {
        //TODO: user config and get rid of magic string
        public readonly AGLWebServiceConfig config;
        public AGLWebService(HttpClient client, IOptions<AGLWebServiceConfig> options) : base(client, options.Value.BaseURL) => config = options.Value;

        public async Task<List<Person>> GetPeople()
        {
            var response = await _client.GetAsync(config.PeopleURI);

            if (!response.IsSuccessStatusCode)
                return null;

            using var responseStream = await response.Content.ReadAsStreamAsync();
            return await responseStream.DeserializeAsync<List<Person>>();
        }
    }
}
