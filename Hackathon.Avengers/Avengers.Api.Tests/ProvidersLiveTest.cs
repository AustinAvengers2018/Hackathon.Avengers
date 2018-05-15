using Avengers.Api.Controllers;
using Avengers.Api.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers.Api.Tests
{
    [TestClass]
    public class ProvidersLiveTest
    {
        [TestMethod]
        public void ProviderController_Get_ShouldReturnListOfProviderEntity()
        {
            var sut = new ProviderController(new AvengersCloudAccess(true));
            var results = sut.Get();
        }
    }
}
