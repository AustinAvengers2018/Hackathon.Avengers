using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Avengers.Mvc.Models
{
    public class AzurePrescriptionEntity : TableEntity
    {
        public AzurePrescriptionEntity(Prescription prescription)
        {
            PrescriptionID = prescription.PrescriptionID;
            ProviderID = prescription.ProviderID;
            Ssn = prescription.Ssn;
            LastName = prescription.LastName;
            FirstName = prescription.FirstName;
            ZipCode = prescription.ZipCode;
            State = prescription.State;
            DrugName = prescription.DrugName;
            DaysSupply = prescription.DaysSupply;
            PrescriptionDate = prescription.PrescriptionDate;
            PartitionKey = "prescription";
            RowKey = ProviderID;
        }
        public AzurePrescriptionEntity()
        { }

        public string PrescriptionID { get; set; }
        public string ProviderID { get; set; }
        public string Ssn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string DrugName { get; set; }
        public int DaysSupply { get; set; }
        public DateTime PrescriptionDate { get; set; }
        
    }
}