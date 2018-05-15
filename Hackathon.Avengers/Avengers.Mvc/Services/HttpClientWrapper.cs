using System;
using System.Net.Http;

namespace Avengers.Mvc.Services
{
    public class HttpClientWrapper
    {
        HttpClient _client;

        public HttpClientWrapper()
        {

        }

        public HttpClientWrapper(HttpClient client)
        {
            _client = client;
        }

        public virtual object GetAsync(string route)
        {
            return _client.GetAsync(route);
        }
    }
}