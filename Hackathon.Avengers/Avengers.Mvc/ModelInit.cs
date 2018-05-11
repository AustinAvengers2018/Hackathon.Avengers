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
            bool deleteAzureTables = Boolean.Parse(WebConfigurationManager.AppSettings["DeleteAzureTables"]);

            List<Provider> providers = null;
            List<Provider> flaggedProviders;
            List<Prescription> prescriptions;
            List<Patient> flaggedPatients;
            

            if (loadExcelDataToAzure)
            {
                providers = LoadProvidersFromExcelData();
                CalculateNationalPercentileRank(providers);
                CalculateStatePercentileRank(providers);
                flaggedProviders = GetFlaggedProviders(providers);

                prescriptions = LoadPrescriptionsFromExcelData();
                flaggedPatients = CalculatePrescriptionFraud(prescriptions);


                var flaggedCount = flaggedProviders.Count();

                SaveProvidersToAzureTable(flaggedProviders);
                SavePatientsToAzureTable(flaggedPatients);

            }
            else
            {
                flaggedProviders = LoadProvidersFromAzureTables();
                flaggedPatients = LoadPatientsFromAzureTables();
            }


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

        public static List<Patient> CalculatePrescriptionFraud(List<Prescription> prescriptions)
        {
            List<Patient> badPatients = new List<Patient>();
            foreach (var prescriptionPerPatient in prescriptions.GroupBy(x => x.Ssn))
            {
                var prescriptionPerPationetSorted = prescriptionPerPatient.OrderBy(d => d.PrescriptionDate);
                Patient potentialBadPatient = new Patient();
                potentialBadPatient.PrescriptionCount = prescriptionPerPationetSorted.Count();
                int innerCount = 1;
                DateTime FirstDayof30 = new DateTime();
                foreach (Prescription p in prescriptionPerPationetSorted)
                {
                    if (FirstDayof30 == null)
                    {
                        FirstDayof30 = p.PrescriptionDate;
                    }
                    else if ((p.PrescriptionDate - FirstDayof30).TotalDays >= 29)
                    {
                        FirstDayof30 = p.PrescriptionDate;
                    }
                    else
                    {
                        innerCount++;
                        if (innerCount == 4)
                        {
                            innerCount = 1;
                            potentialBadPatient.MultipleDetectionCount++;
                            potentialBadPatient.FirstName = p.FirstName;
                            potentialBadPatient.LastName = p.LastName;
                            potentialBadPatient.Ssn = p.Ssn;
                            potentialBadPatient.State = p.State;
                        }    
                    }
                }
                if (potentialBadPatient.MultipleDetectionCount > 0)
                {
                    badPatients.Add(potentialBadPatient);
                }
            }
            return badPatients;
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


        public static void SaveProvidersToAzureTable(List<Provider> providers)
        {
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);

            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();

            CloudTable providerTable = tableClient.GetTableReference("FlaggedProviders");

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

        public static void SavePatientsToAzureTable(List<Patient> patients)
        {
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);

            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();

            CloudTable patientTable = tableClient.GetTableReference("FlaggedPatients");

            patientTable.CreateIfNotExists();


            var batchCount = 1;
            TableBatchOperation batchOperation = new TableBatchOperation();

            Patient lastPatient = patients.Last();

            foreach (Patient p in patients)
            {
                AzurePatientEntity newPatient = new AzurePatientEntity(p);
                TableOperation insert = TableOperation.InsertOrReplace(newPatient);
                batchOperation.InsertOrReplace(newPatient);

                if (batchCount >= 99 || p.Equals(lastPatient))
                {
                    batchCount = 1;
                    patientTable.ExecuteBatch(batchOperation);
                    batchOperation.Clear();
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


        public static List<Provider> LoadProvidersFromAzureTables()
        {
            var providers = new List<Provider>();
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);
            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();
            CloudTable providerTable = tableClient.GetTableReference("FlaggedProviders");

            // Construct the query operation for all provider entities where PartitionKey="provider".
            TableQuery<AzureProviderEntity> query = new TableQuery<AzureProviderEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "provider"));

            foreach (AzureProviderEntity entity in providerTable.ExecuteQuery(query))
            {
                var provider = new Provider(entity);
                providers.Add(provider);
            }
            return providers;
        }
        public static List<Patient> LoadPatientsFromAzureTables()
        {
            var patients = new List<Patient>();
            string storageConnection = System.Configuration.ConfigurationManager.AppSettings.Get("AzureFraudStorageConnectionString");
            CloudStorageAccount storageAcount = CloudStorageAccount.Parse(storageConnection);
            CloudTableClient tableClient = storageAcount.CreateCloudTableClient();
            CloudTable patientTable = tableClient.GetTableReference("FlaggedPatients");

            // Construct the query operation for all provider entities where PartitionKey="provider".
            TableQuery<AzurePatientEntity> query = new TableQuery<AzurePatientEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "patient"));

            foreach (AzurePatientEntity entity in patientTable.ExecuteQuery(query))
            {
                var patient = new Patient(entity);
                patients.Add(patient);
            }
            return patients;
        }


        public static List<Provider> LoadProvidersFromExcelData()
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


        public static List<Prescription> LoadPrescriptionsFromExcelData()
        {
            Excel.Application excel;
            Excel.Workbook workbook;
            Excel.Worksheet worksheet;


            string path = HostingEnvironment.ApplicationPhysicalPath;
            string dataPath = path + "\\Data\\opioid_prescription_data.xlsx";

            excel = new Excel.Application();
            workbook = excel.Workbooks.Open(dataPath);
            worksheet = excel.ActiveSheet;
            Excel.Range xlRange = worksheet.UsedRange;
            var prescriptions = new List<Prescription>();
            int rowcount = xlRange.Rows.Count;
            int colcount = xlRange.Columns.Count;

            for (int i = 2; i <= rowcount; i++)
            {
                String[] pres = new String[9];
                for (int j = 1; j <= colcount; j++)
                {
                    if (j == 9)
                    {
                        pres[j - 1] = xlRange.Cells[i, j].Value.ToString();
                    }
                    else
                    {
                        pres[j - 1] = xlRange.Cells[i, j].Value2.ToString();
                    }
                
                }
                var prescription = new Prescription(pres);
                prescriptions.Add(prescription);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(worksheet);
            workbook.Close();
            Marshal.ReleaseComObject(workbook);
            excel.Quit();
            Marshal.ReleaseComObject(excel);

            //Insure list is unique by Prescription
            var hash = new HashSet<Prescription>(prescriptions);
            return hash.ToList();
        }
    }
}