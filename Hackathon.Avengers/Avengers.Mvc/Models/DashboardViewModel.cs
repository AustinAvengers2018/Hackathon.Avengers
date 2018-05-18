using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Mvc.Models
{
    public class DashboardViewModel
    {
        public IEnumerable<DashboardEntity> Entities { get; set; }
    }

    public class DashboardEntity
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Type { get; set; }
        public object Entity { get; set; }
    }
}