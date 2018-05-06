using Avengers.Api.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.DataAccess
{
    public class ProvidersGateway : IGateway<ProviderEntity>
    {
        string _partitionKey = "provider";
        private CloudTable Providers;

        public ProvidersGateway(IAvengersCloudAccess account)
        {
            Providers = account.GetTableFor<ProviderEntity>();
        }

        public void Create(ProviderEntity newobject)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ProviderEntity Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProviderEntity> Get()
        {
            var query = GetAllQuery();
            return Providers.ExecuteQuery(query);
        }

        private TableQuery<ProviderEntity> GetAllQuery()
        {
            return new TableQuery<ProviderEntity>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, _partitionKey));
        }

        public void Update(int id, ProviderEntity tObject)
        {
            throw new NotImplementedException();
        }

        ProviderEntity IGateway<ProviderEntity>.Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}