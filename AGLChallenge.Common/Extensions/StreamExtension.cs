using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AGLChallenge.Common.Extensions
{
    public static class StreamExtension
    {
        private static JsonSerializerOptions DefaultSerializerSettings => new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };


        public static async Task<T> DeserializeAsync<T>(this Stream responseStream)
        {
            return await JsonSerializer.DeserializeAsync<T>(responseStream, DefaultSerializerSettings);
        }
    }
}
