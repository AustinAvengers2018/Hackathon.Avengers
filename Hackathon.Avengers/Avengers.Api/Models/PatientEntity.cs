using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.Models
{
    public class PatientEntity : TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MultipleDetectionCount { get; set; }
        public int PrescriptionCount { get; set; }
        public string SSN { get; set; }
        public bool  Reviewed { get; set; }
    }
}