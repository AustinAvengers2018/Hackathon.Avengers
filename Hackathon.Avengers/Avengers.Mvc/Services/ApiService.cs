using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Avengers.Mvc.Services
{
    public abstract class ApiService
    {
        HttpClientWrapper _client;

        public ApiService(HttpClientWrapper apiClient)
        {
            _client = apiClient;
        }

        public JToken GetAndParseResponse(string route)
        {
            var response = (_client.GetAsync(route) as Task<HttpResponseMessage>).Result;
            return response.Content.ReadAsAsync<JToken>().Result;
        }

    }
}