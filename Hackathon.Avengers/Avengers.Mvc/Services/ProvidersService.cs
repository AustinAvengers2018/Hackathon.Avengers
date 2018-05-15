using Avengers.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net.Http;
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
            return new List<Provider>();
        }
    }
}