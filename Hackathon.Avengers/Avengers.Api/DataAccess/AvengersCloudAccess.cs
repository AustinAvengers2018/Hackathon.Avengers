using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Avengers.Api.Models;

namespace Avengers.Api.DataAccess
{
    public class AvengersCloudAccess : IAvengersCloudAccess
    {
        CloudStorageAccount _storageAccount;
        CloudTableClient _tableClient;

        public AvengersCloudAccess()
        {

        }

        public AvengersCloudAccess(bool shouldInitialize)
        {
            if (shouldInitialize)
            {
                var avengersCnxn = CloudConfigurationManager.GetSetting("avengersTables");
                _storageAccount = CloudStorageAccount.Parse(avengersCnxn);
                _tableClient = _storageAccount.CreateCloudTableClient();
            }
        }

        public IGateway<T> GetGatewayFor<T>()
        {
            var typeName = typeof(T).Name;
            switch (typeName)
            {
                case "ProviderEntity":
                    return new ProvidersGateway(this) as IGateway<T>;
                default:
                    return new NullGateway() as IGateway<T>;
            }
        }

        public CloudTable GetTableFor<T>()
        {
            var typeName = typeof(T).Name;
            switch (typeName)
            {
                case "ProviderEntity":
                    return _tableClient.GetTableReference("ProviderRawData");
                default:
                    return new CloudTable(new Uri("http://null.org"));
            }
        }

    }
}