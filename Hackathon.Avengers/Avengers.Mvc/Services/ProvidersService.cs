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
    public class ProvidersService: ApiService
    {

        public ProvidersService(HttpClientWrapper apiClient) : base(apiClient)
        {
        }

        public IEnumerable<Provider> GetProviders()
        {
            var jTokenResult = GetAndParseResponse("Provider");
            List<Provider> lProviders = new List<Provider>();
            foreach( var obj in jTokenResult )
            {
                lProviders.Add( new Provider(obj.ToObject<AzureProviderEntity>()));
            }
            return lProviders;
        }

         internal Provider GetProvider(string id)
        {
            var jTokenResult = GetAndParseResponse($"Provider/{id}");
            return new Provider(jTokenResult.First().ToObject<AzureProviderEntity>());
        }
    }
}