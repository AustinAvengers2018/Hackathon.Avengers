using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;

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

        public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
        {
            base.ReadEntity(properties, operationContext);
            PartitionKey = "patient";
            RowKey = SSN = properties["Ssn"].StringValue;
            FirstName = properties["FirstName"].StringValue;
            LastName = properties["LastName"].StringValue;
            MultipleDetectionCount = properties["MultipleDetectionCount"].Int32Value ?? int.MinValue;
            PrescriptionCount = properties["PrescriptionCount"].Int32Value ?? int.MinValue;
            Reviewed = properties["Reviewed"].BooleanValue ?? false;
        }
    }
}