using Avengers.Mvc.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Avengers.Mvc.Services
{
    public class PatientsService : ApiService
    {
        HttpClientWrapper _client;

        public PatientsService(HttpClientWrapper apiClient): base(apiClient)
        {
        }


        public IEnumerable<Patient> GetPatients()
        {
            var jTokenResult = GetAndParseResponse("Patient");
            List<Patient> patientList = new List<Patient>();
            foreach (var obj in jTokenResult)
            {
                patientList.Add(new Patient(obj.ToObject<AzurePatientEntity>()));
            }
            return patientList;
        }

        internal object GetPatient(string id)
        {
            var jTokenResult = GetAndParseResponse($"Patient/{id}");
            return new Patient(jTokenResult.First().ToObject<AzurePatientEntity>());
        }
    }
}