using Avengers.Api.Models;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.DataAccess
{
    public class FraudStorageRepository
    {

        public FraudStorageRepository(IAvengersCloudAccess cloud)
        {
            Providers = cloud.GetGatewayFor<ProviderEntity>();
            Patients = cloud.GetGatewayFor<PatientEntity>();
        }

        public IGateway<ProviderEntity> Providers { get; private set; }
        public IGateway<PatientEntity> Patients { get; set; }

    }
}