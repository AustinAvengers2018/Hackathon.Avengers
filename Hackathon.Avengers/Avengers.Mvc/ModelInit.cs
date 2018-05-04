using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Excel = Microsoft.Office.Interop.Excel;
using Avengers.Mvc.Models;
using System.Runtime.InteropServices;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Web.Configuration;
using System.Diagnostics;


namespace Avengers.Mvc
{
    public static class ModelInit
    {

        public static void LoadData()
        {
            bool loadExcelDataToAzure = Boolean.Parse(WebConfigurationManager.AppSettings["LoadExcelDataToAzure"]);
            List<Provider> providers;
            List<Provider> flaggedProviders;

            if (loadExcelDataToAzure)
            {
                providers = LoadFromExcelData();
                CalculateNationalPercentileRank(providers);
                CalculateStatePercentileRank(providers);
                flaggedProviders = GetFlaggedProviders(providers);

                var flaggedCount = flaggedProviders.Count();

                SaveDataToAzureTable(flaggedProviders);
            }
            else
            {
                providers = LoadFromAzureTables();
            }

            //Do Calculations


            bool deleteAzureTables = Boolean.Parse(WebConfigurationManager.AppSettings["DeleteAzureTables"]);
            if (deleteAzureTables) DeleteAzureTableItems(providers);
        }

        public static List<Provider> GetFlaggedProviders(List<Provider> providers)
        {
            var providersBySpecialty = providers.Where(d => d.NationalRank >= d.National99Percentile && d.StateRank >= d.State99Percentile && d.ProviderID != null);
            return providersBySpecialty.ToList();
        }

        public static void CalculateNationalPercentileRank(List<Provider> providers)
        {

            foreach (var providersBySpecialty in providers.GroupBy(y => y.Specialty))
            {
                var providersBySpecialtySorted = providersBySpecialty.OrderBy(d => d.OpioidRateD);

                int providerCount = providersBySpecialtySorted.Count();
                decimal percent90 = 0.99m;
                //var percentile = providerCount * percent90;
                var percentile = Convert.ToInt32(Math.Round(providerCount * percent90, 0));
                var i = 1;
                foreach (Provider g in providersBySpecialtySorted)
                {

                    g.NationalRank = i;
                    i++;
                    g.National99Percentile = percentile;
                }
                //Debug.WriteLine("Specialty: {0}", s.SpecialtyName);
            }
        }



        public static void CalculateStatePercentileRank(List<Provider> providers)
        {
            foreach (var providersbyState in providers.GroupBy(x => x.State))
            {
                foreach (var providersbyStateSpecialty in providersbyState.GroupBy(y => y.Specialty))
                {
                    var providersByStateSpecialtySorted = providersbyStateSpecialty.OrderBy(d => d.OpioidRateD);
                    int providerCount = providersByStateSpecialtySorted.Count();
                    decimal percent99 = 0.99m;
                    var percentile = Convert.ToInt32(Math.Round(providerCount * percent99, 0));
                    var i = 1;
                    foreach (Provider g in providersByStateSpecialtySorted)
                    {
                        g.StateRank = i;
                        i++;
                        g.State99Percentile = percentile;
                    }

                }
            }
        }


        public static List<State> GetStates(List<Provider> providers)
        {
            List<State> allProviderStates = providers
                                   .Select(s => new State() { StateName = s.State }).ToList();
            var hash = new HashSet<State>(allProviderStates);
            return hash.ToList();
        }

        public static List<Specialty> GetSpecialties(List<Provider> providers)
        {
            List<Specialty> allSpecialties = providers
                                   .Select(s => new Specialty() { SpecialtyName = s.Specialty }).ToList();
            var hash = new HashSet<Specialty>(allSpecialties);
            return hash.ToList();
        }


        public static void SaveDataToAzureTable(List<Provider> providers)
        {
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);

            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();

            CloudTable providerTable = tableClient.GetTableReference("ProviderRawData");

            providerTable.CreateIfNotExists();


            var batchCount = 1;
            TableBatchOperation batchOperation = new TableBatchOperation();

            Provider lastProvider = providers.Last();

            foreach (Provider p in providers)
            {
                AzureProviderEntity newProvider = new AzureProviderEntity(p);
                TableOperation insert = TableOperation.InsertOrReplace(newProvider);
                batchOperation.InsertOrReplace(newProvider);

                //providerTable.Execute(insert);
                //Change 100 to 10 to test small batch
                if (batchCount >= 99 || p.Equals(lastProvider))
                {
                    batchCount = 1;
                    providerTable.ExecuteBatch(batchOperation);
                    batchOperation.Clear();
                    //break foreach  when one batch of 10 is done
                    //break;
                    Debug.WriteLine("Batch stored to azure: {0}", batchCount);
                }
                else
                {
                    batchCount++;
                }
            }
        }

        public static void DeleteAzureTableItems(List<Provider> providers)
        {
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);
            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();
            CloudTable providerTable = tableClient.GetTableReference("ProviderRawData");

            var deleteCount = 0;
            foreach (Provider p in providers)
            {
                TableOperation retrieveOperation = TableOperation.Retrieve<AzureProviderEntity>("provider", p.ProviderID.ToString());
                TableResult retrievedResult = providerTable.Execute(retrieveOperation);
                AzureProviderEntity deleteEntity = (AzureProviderEntity)retrievedResult.Result;
                if (deleteEntity != null)
                {
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                    // Execute the operation.
                    providerTable.Execute(deleteOperation);
                    deleteCount++;
                    Console.WriteLine("Entity deleted.");
                }
                //Testing code
                //if (deleteCount == 10) break;
            }
            Console.WriteLine("deleted {0} entities.", deleteCount);
        }


        public static List<Provider> LoadFromAzureTables()
        {
            var providers = new List<Provider>();
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);
            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();
            CloudTable providerTable = tableClient.GetTableReference("ProviderRawData");

            // Construct the query operation for all provider entities where PartitionKey="provider".
            TableQuery<AzureProviderEntity> query = new TableQuery<AzureProviderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "provider"));

            foreach (AzureProviderEntity entity in providerTable.ExecuteQuery(query))
            {
                var provider = new Provider(entity);
                providers.Add(provider);
            }
            return providers;
        }


        public static List<Provider> LoadFromExcelData()
        {
            Excel.Application excel;
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;


            string path = HostingEnvironment.ApplicationPhysicalPath;
            string dataPath = path + "\\Data\\Medicare_Part_D_Opioid_Prescriber_Summary_File_2016.csv";

            excel = new Excel.Application();
            workbook = excel.Workbooks.Open(dataPath);
            worksheet = excel.ActiveSheet;
            var rowcount = worksheet.Rows.Count;

            var firstRow = 2;
            var lastRow = rowcount;
            var batchSize = 5000;
            var batches = Enumerable
                .Range(0, (int)Math.Ceiling((lastRow - firstRow) / (double)batchSize))
                .Select(x =>
                    string.Format(
                        "A{0}:K{1}",
                        x * batchSize + firstRow,
                        Math.Min((x + 1) * batchSize + firstRow - 1, lastRow)))
                .Select(range => ((Array)worksheet.Range[range].Cells.Value));


            var providers = new List<Provider>();


            foreach (var batch in batches)
            {
                foreach (var item in batch)
                {
                    if (item != null)
                    {
                        var srow = item;
                        String[] rowitems = srow.ToString().Split(new char[] { ';' });
                        if (rowitems.Length >= 9 && !string.IsNullOrWhiteSpace(rowitems[3])  && !string.IsNullOrWhiteSpace(rowitems[4])  && !string.IsNullOrWhiteSpace(rowitems[0]))
                        {
                            var provider = new Provider(rowitems);
                            providers.Add(provider);
                        }
                    }
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(worksheet);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            excel.Quit();
            Marshal.ReleaseComObject(excel);

            //Insure list is unique by ProviderID
            var hash = new HashSet<Provider>(providers);
            return hash.ToList();
        }
    }
}