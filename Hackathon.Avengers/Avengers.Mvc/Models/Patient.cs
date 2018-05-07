using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Avengers.Mvc.Models
{
    public class Patient : IEquatable<Patient>
    {
        public string Ssn { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string State { get; set; }
        public int PrescriptionCount { get; set; }
        public int MultipleDetectionCount {get; set;}

        public Patient(AzurePatientEntity entity)
        {
            Ssn = entity.Ssn;
            LastName = entity.LastName;
            FirstName = entity.FirstName;
            State = entity.State;
            PrescriptionCount = entity.PrescriptionCount;
            MultipleDetectionCount = entity.MultipleDetectionCount;
        }

        public Patient() { }

        public Patient(String[] items)
        {
            //if (items.Length == 9 && !string.IsNullOrWhiteSpace(items[0]))
            //{
            //    Ssn = items[0];
            //    FirstName = items[1];
            //    LastName = items[2];
            //    ZipCode = items[3];
            //    PrescriptionID = items[4];
            //    ProviderID = items[5];
            //    DrugName = items[6];
            //    DaysSupply = Convert.ToInt32(items[7]);
            //    PrescriptionDate = Convert.ToDateTime(items[8]);
            //}
        }

        public bool Equals(Patient other)
        {
            if (Ssn == other.Ssn)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashFirstName = Ssn == null ? 0 : Ssn.GetHashCode();
            return hashFirstName;
        }
    }
}