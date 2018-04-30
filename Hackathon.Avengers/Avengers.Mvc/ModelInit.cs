using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using Excel = Microsoft.Office.Interop.Excel;
using Avengers.Mvc.Models;
using System.Runtime.InteropServices;

namespace Avengers.Mvc
{
    public static class ModelInit
    {

        public static void LoadData()
        {
            var Providers = LoadExcel();
        }

        public static void  LoadAzureTables( List<Provider> providers)
        {

        }


        public static List<Provider> LoadExcel()
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

                    var provider = new Provider(rowitems);
                    providers.Add(provider);
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