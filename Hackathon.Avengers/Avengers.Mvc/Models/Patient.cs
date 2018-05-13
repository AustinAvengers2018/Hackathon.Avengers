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
        public bool Reviewed { get; set; }

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