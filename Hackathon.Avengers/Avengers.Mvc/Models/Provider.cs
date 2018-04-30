using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Avengers.Mvc.Models
{
    public class Provider
    {
        public long ProviderID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Specialty { get; set; }
        public int Total { get; set; }
        public int  Opioid { get; set; }
        public string OpioidRateS { get; set; }
        public decimal OpioidRateD { get; set; }
        public int ExtendedOpioid { get; set; }
        public string ExtendedOpioidRateS { get; set; }
        public decimal ExtendedOpioidRateD { get; set; }

        public Provider(String[] items)
        {
            long ProviderIDL;
            if (items.Length >= 9 && items.Length < 12 &&  items[0] != "" && long.TryParse(items[0], out ProviderIDL))
            {
                ProviderID = Convert.ToInt64(items[0]);
                LastName = items[1];
                FirstName = items[2];
                ZipCode = items[3];
                State = items[4];
                Specialty = items[5];
                Total = Convert.ToInt32(items[6]);
                if (items.Length > 7 && items[7] != "")
                {
                    Opioid = Convert.ToInt32(items[7]);
                }
                if (items.Length > 8 && items[8] != "" )
                {
                    OpioidRateS = items[8];
                    OpioidRateD = decimal.Parse(OpioidRateS.TrimEnd(new char[] { '%', ' ' }));
                }

                if (items.Length > 9 && items[9] != "" ) 
                {
                    ExtendedOpioid = Convert.ToInt32(items[9]);
                }
                if (items.Length > 10 && items[10] != "")
                {
                    ExtendedOpioidRateS = items[10];
                    ExtendedOpioidRateD = decimal.Parse(ExtendedOpioidRateS.TrimEnd(new char[] { '%', ' ' }));
                }
            }

        }
    }
}
