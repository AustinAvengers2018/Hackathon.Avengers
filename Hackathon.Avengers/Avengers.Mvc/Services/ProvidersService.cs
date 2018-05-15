using Avengers.Mvc.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Avengers.Mvc.Services
{
    public class ProvidersService
    {
        HttpClientWrapper _client;

        public ProvidersService(HttpClientWrapper apiClient)
        {
            _client = apiClient;
        }

        public IEnumerable<Provider> GetProviders()
        {
            var response = (_client.GetAsync("Provider") as Task<HttpResponseMessage>).Result;
            var content = response.Content.ReadAsAsync<JToken>().Result;
            List<Provider> lProviders = new List<Provider>();
            foreach( var obj in content )
            {
                lProviders.Add( new Provider(obj.ToObject<AzureProviderEntity>()));
            }
            return lProviders;
        }
    }
}