using Avengers.Api.DataAccess;
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
        readonly FraudStorageRepository _repo;

        public ProviderController(IAvengersCloudAccess cloud)
        {
            _repo = new FraudStorageRepository(cloud);
        }

        // GET: api/Provider
        public IEnumerable<ProviderEntity> Get()
        {
            return _repo.Providers.Get();
        }

        // GET: api/Provider/5
        public ProviderEntity Get(string npi)
        {
            return _repo.Providers.Find(npi);
        }

        // POST: api/Provider
        public void Post([FromBody]ProviderEntity value)
        {
            _repo.Providers.Create(value);
        }

        // PUT: api/Provider/5
        public void Put(long npi, [FromBody]ProviderEntity value)
        {
            _repo.Providers.Update(npi.ToString(), value);
        }

        // DELETE: api/Provider/5
        public void Delete(long npi)
        {
            _repo.Providers.Delete(npi.ToString());
        }
    }
}
