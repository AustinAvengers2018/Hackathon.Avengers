﻿using Avengers.Api.Models;
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
        public IEnumerable<PatientEntity> Get()
        {
            return new PatientEntity[] {  };
        }

        // GET: api/Prescription/5
        public PatientEntity Get(int rxNumber)
        {
            return new PatientEntity();
        }

        // POST: api/Prescription
        public void Post([FromBody]PatientEntity rx)
        {
        }

        // PUT: api/Prescription/5
        public void Put(int rxNumber, [FromBody]PatientEntity rx)
        {
        }

        // DELETE: api/Prescription/5
        public void Delete(int id)
        {
        }
    }
}
