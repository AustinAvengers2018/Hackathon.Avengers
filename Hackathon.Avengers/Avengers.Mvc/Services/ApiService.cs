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
            try
            {
                var response = (_client.GetAsync(route) as Task<HttpResponseMessage>).Result;
                return response.Content.ReadAsAsync<JToken>().Result;
            }
            catch (TaskCanceledException ex)
            {
                // Check ex.CancellationToken.IsCancellationRequested here.
                // If false, it's pretty safe to assume it was a timeout.
                return false;
            }
        }

    }
}