using Avengers.Api.DataAccess;
using StructureMap;
using StructureMap.Graph;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Avengers.Api.App_Start
{
    public class AvengersIocRegistry: Registry
    {
        public AvengersIocRegistry()
        {
            Scan(scan => {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            For<IAvengersCloudAccess>().Use(new AvengersCloudAccess(true));
        }
    }
}