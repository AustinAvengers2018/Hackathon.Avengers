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

namespace Avengers.Mvc
{
    public static class ModelInit
    {

        public static void LoadData()
        {
            bool loadExcelDataToAzure = Boolean.Parse(WebConfigurationManager.AppSettings["LoadExcelDataToAzure"]);
            List<Provider> providers;


            if (loadExcelDataToAzure)
            {
                providers = LoadFromExcelData();

                List<State> allProviderStates  = providers
                                   .Select(s => new State() { StateName = s.State}).ToList();

                //allProviderStates = allProviderStates.Distinct().ToList();

                

                var hash = new HashSet<State>(allProviderStates);


                SaveDataToAzureTable(providers);   
            }

            bool deleteAzureTables = Boolean.Parse(WebConfigurationManager.AppSettings["DeleteAzureTables"]);

            //DeleteAzureTableItems(providers);
        }

        public static void  SaveDataToAzureTable( List<Provider> providers)
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

                providerTable.Execute(insert);
                //Change 100 to 10 to test small batch
                if (batchCount == 100 || p.Equals(lastProvider))
                {
                    providerTable.ExecuteBatch(batchOperation);
                    batchCount = 1;
                    //break to  when one batch of 10 is done
                    //break;
                }
                else
                {
                    batchCount++;
                }
            }
        }

        public static void DeleteAzureTableItems (List<Provider> providers)
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
            Console.WriteLine("deleted {0} entities.",deleteCount);
        }


        //public static List<Provider> LoadFromAzureTableData()
        //{

        //}


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
                    String[] rowitems = srow.ToString().Split(new char[] {';'});
                        if (rowitems.Length >= 9)
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

            return providers;

        }
    }
}