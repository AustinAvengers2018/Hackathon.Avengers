using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Avengers.Mvc.Models;

namespace Avengers.Mvc.Models
{
    public class AzureProviderEntity : TableEntity
    {
        public AzureProviderEntity ( Provider provider)
        {
            ProviderID = provider.ProviderID;
            LastName = provider.LastName;
            FirstName = provider.FirstName;
            ZipCode = provider.ZipCode;
            State = provider.State;
            Specialty = provider.Specialty;
            Total = provider.Total;
            Opioid = provider.Opioid;
            OpioidRate = provider.OpioidRateD;
            ExtendedOpioid = provider.ExtendedOpioid;
            ExtendedOpioidRate = provider.ExtendedOpioidRateD;
            PartitionKey = "provider";
            RowKey = ProviderID.ToString();
        }
        public AzureProviderEntity()
        { }

        public long ProviderID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Specialty { get; set; }
        public int Total { get; set; }
        public int Opioid { get; set; }
        public decimal OpioidRate { get; set; }
        public int ExtendedOpioid { get; set; }
        public decimal ExtendedOpioidRate { get; set; }
    }
}