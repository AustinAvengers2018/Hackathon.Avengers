using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.Models
{
    public class Prescription
    {
        public int RxNumber { get; set; }
        public DateTime RxFillDate { get; set; }

        public string SSN { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public int ZIPCode { get; set; }

        public int PrescriberNPI { get; set; }
        public string DrugName { get; set; }
        public int DaysSupply { get; set; }
    }
}