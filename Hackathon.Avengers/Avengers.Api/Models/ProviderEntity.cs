using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.Models
{
    public class ProviderEntity : TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long National99Percentile { get; set; }
        public long NationalRank { get; set; }
        public int Opiod { get; set; }
        public long ProviderID { get; set; }
        public bool Reviewed { get; set; }
        public string Specialty { get; set; }
        public int ExtendedOpiod { get; set; }
        public string State { get; set; }
        public long State99Percentile { get; set; }
        public int StateRank { get; set; }
        public int Total { get; set; }
        public string ZipCode { get; set; }

    }
}