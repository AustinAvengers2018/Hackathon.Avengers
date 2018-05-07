using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.Models
{
    public class ProviderEntity: TableEntity
    {
        public int ProviderID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Specialty { get; set; }
        public int ZIPCode { get; set; }
        public string State { get; set; }
        public int TotalClaimCount { get; set; }
        public int OpiodClaimCount { get; set; }
        public float OpioidPrescribingRate { get; set; }
        public int ExtendedReleaseOpiodClaimCount { get; set; }
        public float ExtendedReleaseOpiodPrescribingRate { get; set; }

    }
}