using Avengers.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Avengers.Api.Controllers
{
    public class PrescriptionController : ApiController
    {
        // GET: api/Prescription
        public IEnumerable<PrescriptionEntity> Get()
        {
            return new PrescriptionEntity[] {  };
        }

        // GET: api/Prescription/5
        public PrescriptionEntity Get(int rxNumber)
        {
            return new PrescriptionEntity();
        }

        // POST: api/Prescription
        public void Post([FromBody]PrescriptionEntity rx)
        {
        }

        // PUT: api/Prescription/5
        public void Put(int rxNumber, [FromBody]PrescriptionEntity rx)
        {
        }

        // DELETE: api/Prescription/5
        public void Delete(int id)
        {
        }
    }
}
