using Avengers.Api.DataAccess;
using Avengers.Api.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avengers.Api.Tests
{
    [TestFixture]
    public class ProvidersGatewayTests
    {
        [Test]
        public void Get_ShouldReturnAllRecords()
        {
            var fakeAccess = new Mock<IAvengersCloudAccess>();
            var fakeProviderTable = new Mock<CloudTable>();
            fakeAccess.Setup(x => x.GetTableFor<ProviderEntity>())
                .Returns(fakeProviderTable.Object);

            var sut = new ProvidersGateway(fakeAccess.Object);
            var providersCollection = sut.Get();

            Assert.IsNotNull(providersCollection);
            Assert.IsTrue(providersCollection.Count() > 0);
        }
    }
}
