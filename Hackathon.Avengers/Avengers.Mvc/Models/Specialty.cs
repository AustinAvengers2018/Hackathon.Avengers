using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Mvc.Models
{
    public class Specialty : IEquatable<Specialty>

    {
        public string SpecialtyName { get; set; }


        public bool Equals(Specialty other)
        {
            if (SpecialtyName == other.SpecialtyName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashFirstName = SpecialtyName == null ? 0 : SpecialtyName.GetHashCode();
            return hashFirstName;
        }

    }
}