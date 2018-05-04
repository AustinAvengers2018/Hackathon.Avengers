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
            NationalRank = provider.NationalRank;
            National99Percentile = provider.National99Percentile;
            StateRank = provider.StateRank;
            State99Percentile = provider.State99Percentile;
            PartitionKey = "provider";
            RowKey = ProviderID;
        }
        public AzureProviderEntity()
        { }

        public string ProviderID { get; set; }
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
        public int NationalRank { get; set; }
        public int National99Percentile { get; set; }
        public int StateRank { get; set; }
        public int State99Percentile { get; set; }
    }
}