using Avengers.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Avengers.Api.Controllers
{
    public class ProviderController : ApiController
    {
        // GET: api/Provider
        public IEnumerable<Provider> Get()
        {
            return new Provider[] { };
        }

        // GET: api/Provider/5
        public Provider Get(int npi)
        {
            return new Provider();
        }

        // POST: api/Provider
        public void Post([FromBody]Provider value)
        {
        }

        // PUT: api/Provider/5
        public void Put(int npi, [FromBody]Provider value)
        {
        }

        // DELETE: api/Provider/5
        public void Delete(int npi)
        {
        }
    }
}
