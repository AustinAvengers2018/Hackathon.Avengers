using Avengers.Api.DataAccess;
using Avengers.Api.Models;
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
    public class FraudStorageRepositoryTests
    {
        [Test]
        public void Gateways_ShouldBeCreatedWithRepo()
        {
            var fakeAccess = new Mock<IAvengersCloudAccess>();
            var fakeProviderGateway = new Mock<IGateway<ProviderEntity>>();
            fakeAccess.Setup(x => x.GetGatewayFor<ProviderEntity>())
                .Returns(fakeProviderGateway.Object);
            var fakePrescriptionGateway = new Mock<IGateway<PatientEntity>>();
            fakeAccess.Setup(x => x.GetGatewayFor<PatientEntity>())
                .Returns(fakePrescriptionGateway.Object);
            var repo = new FraudStorageRepository(fakeAccess.Object);
            Assert.IsNotNull(repo.Providers);
            Assert.IsNotNull(repo.Patients);
        }

        [Test]
        public void Providers_WhenUnderlyingTableHasRows_ShouldContainRows()
        {
            var fakeAccess = new Mock<IAvengersCloudAccess>();
            var fakeProviderGateway = new Mock<IGateway<PatientEntity>>();
            fakeAccess.Setup(x => x.GetGatewayFor<PatientEntity>())
                .Returns(fakeProviderGateway.Object);
            var repo = new FraudStorageRepository(fakeAccess.Object);
            Assert.IsNotNull(repo.Patients);
        }

    }
}
