using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Avengers.Mvc.Models
{
    public class Prescription : IEquatable<Prescription>
    {
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
 
        public Prescription(AzurePrescriptionEntity entity)
        {
            PrescriptionID = entity.PrescriptionID;
            ProviderID = entity.ProviderID;
            Ssn = entity.Ssn;
            LastName = entity.LastName;
            FirstName = entity.FirstName;
            ZipCode = entity.ZipCode;
            State = entity.State;
            DrugName = entity.DrugName;
            DaysSupply = entity.DaysSupply;
            PrescriptionDate = entity.PrescriptionDate;
        }

        public Prescription() { }

        public Prescription(String[] items)
        {
            if (items.Length == 9 && !string.IsNullOrWhiteSpace(items[0]))
            {
                Ssn = items[0];
                FirstName = items[1];
                LastName = items[2];
                ZipCode = items[3];
                PrescriptionID = items[4];
                ProviderID = items[5];
                DrugName = items[6];
                DaysSupply = Convert.ToInt32(items[7]);
                PrescriptionDate = Convert.ToDateTime(items[8]);
            }
        }

        public bool Equals(Prescription other)
        {
            if (PrescriptionID == other.PrescriptionID)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashFirstName = PrescriptionID == null ? 0 :PrescriptionID.GetHashCode();
            return hashFirstName;
        }
    }
}
