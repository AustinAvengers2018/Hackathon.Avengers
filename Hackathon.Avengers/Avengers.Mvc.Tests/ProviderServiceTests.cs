using Avengers.Mvc.Models;
using Avengers.Mvc.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Avengers.Mvc.Tests
{
    [TestClass]
    public class ProviderServiceTests
    {
        [TestMethod]
        public void GetProviders_ShouldReturnProviderList()
        { 

            var fakeResponse = new Mock<HttpResponseMessage>();
            var fakeClient = new Mock<HttpClientWrapper>();
            fakeClient.Setup(c => c.GetAsync("Provider")).Returns(fakeResponse.Object);

            var sut = new ProvidersService(fakeClient.Object);
            var result = sut.GetProviders();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Provider>));
        }
    }
}
