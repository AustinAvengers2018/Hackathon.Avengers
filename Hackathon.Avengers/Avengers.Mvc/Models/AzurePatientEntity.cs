using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Avengers.Mvc.Models
{
    public class AzurePatientEntity : TableEntity
    {
        public AzurePatientEntity(Patient patient)
        {
            Ssn = patient.Ssn;
            LastName = patient.LastName;
            FirstName = patient.FirstName;
            State = patient.State;
            PrescriptionCount = patient.PrescriptionCount;
            MultipleDetectionCount = patient.MultipleDetectionCount;
            PartitionKey = "patient";
            RowKey = Ssn;
        }
        public AzurePatientEntity()
        { }
        public string Ssn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string State { get; set; }
        public int PrescriptionCount { get; set; }
        public int DaysSupply { get; set; }
        public int MultipleDetectionCount { get; set; }
    }
}