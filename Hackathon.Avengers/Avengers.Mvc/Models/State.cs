using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Mvc.Models
{
    public class State : IEquatable<State>

    {
        public string StateName { get; set; }


        public bool Equals(State other)
        {
            if (StateName == other.StateName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            int hashFirstName = StateName == null ? 0 : StateName.GetHashCode();
            

            return hashFirstName;
        }

    }
}