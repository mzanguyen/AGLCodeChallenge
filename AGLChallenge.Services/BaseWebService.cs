using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace AGLChallenge.Services
{
    public abstract class BaseWebService
    {
        protected readonly HttpClient _client;
        public BaseWebService(HttpClient client, string address)
        {
            client.BaseAddress = new Uri(address);
            //TODO: Add headers 
            _client = client;
        }
    
    }
}
