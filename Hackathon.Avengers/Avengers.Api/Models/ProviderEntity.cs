using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;

namespace Avengers.Api.Models
{
    public class ProviderEntity : TableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTime Timestamp { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int National99Percentile { get; set; }
        public long NationalRank { get; set; }
        public int Opioid { get; set; }
        public string ProviderID { get; set; }
        public bool Reviewed { get; set; }
        public string Specialty { get; set; }
        public int ExtendedOpioid { get; set; }
        public string State { get; set; }
        public long State99Percentile { get; set; }
        public int StateRank { get; set; }
        public int Total { get; set; }
        public string ZipCode { get; set; }


        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);
            PartitionKey = "provider";
            RowKey = properties["ProviderID"].StringValue;
            //Timestamp = properties["Timestamp"].DateTime ?? DateTime.Today.AddYears(-50);
            FirstName = properties["FirstName"].StringValue;
            LastName = properties["LastName"].StringValue;
            National99Percentile = properties["National99Percentile"].Int32Value ?? Int32.MinValue;
            NationalRank = properties["NationalRank"].Int32Value ?? Int32.MinValue;
            Opioid = properties["Opioid"].Int32Value ?? Int32.MinValue;
            ProviderID = properties["ProviderID"].StringValue;
            Reviewed = properties["Reviewed"].BooleanValue ?? false;
            Specialty = properties["Specialty"].StringValue;
            ExtendedOpioid = properties["ExtendedOpioid"].Int32Value ?? Int32.MinValue;
            State = properties["State"].StringValue;
            State99Percentile = properties["State99Percentile"].Int32Value ?? Int32.MinValue;
            StateRank = properties["StateRank"].Int32Value ?? Int32.MinValue;
            Total = properties["Total"].Int32Value ?? Int32.MinValue;
            ZipCode = properties["ZipCode"].StringValue;
        }
    }
}