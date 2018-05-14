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
    public class PatientController : ApiController
    {

        readonly FraudStorageRepository _repo;

        public PatientController(IAvengersCloudAccess cloud)
        {
            _repo = new FraudStorageRepository(cloud);
        }

        // GET: api/Prescription
        public IEnumerable<PatientEntity> Get()
        {
            return _repo.Patients.Get();
        }

        // GET: api/Prescription/5
        public PatientEntity Get(string rxNumber)
        {
            return _repo.Patients.Find(rxNumber);
        }

        // POST: api/Prescription
        public void Post([FromBody]PatientEntity rx)
        {
            _repo.Patients.Create(rx);
        }

        // PUT: api/Prescription/5
        public void Put(string rxNumber, [FromBody]PatientEntity rx)
        {
            _repo.Patients.Update(rxNumber, rx);
        }

        // DELETE: api/Prescription/5
        public void Delete(string id)
        {
            _repo.Patients.Delete(id);
        }
    }
}
