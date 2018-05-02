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
        public IEnumerable<Prescription> Get()
        {
            return new Prescription[] {  };
        }

        // GET: api/Prescription/5
        public Prescription Get(int rxNumber)
        {
            return new Prescription();
        }

        // POST: api/Prescription
        public void Post([FromBody]Prescription rx)
        {
        }

        // PUT: api/Prescription/5
        public void Put(int rxNumber, [FromBody]Prescription rx)
        {
        }

        // DELETE: api/Prescription/5
        public void Delete(int id)
        {
        }
    }
}
