using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.IO;

namespace AGLChallenge.Services.Test.Helpers
{
    public static class LoadJsonToObject
    {
        public static T FromFile<T>(string filePath)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var jsonString = FromFileToString(filePath);

            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }

        public static string FromFileToString(string filePath)
        {
            return File.ReadAllText(GetPath(filePath));

            static string GetPath(string file) => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), file);
        }
        

    }
}
