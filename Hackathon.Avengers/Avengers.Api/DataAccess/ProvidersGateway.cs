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
            var insert = TableOperation.Insert(newobject);
            Providers.Execute(insert);
        }

        public void Delete(string id)
        {
            var toBeDeleted = Providers.CreateQuery<ProviderEntity>()
                .FirstOrDefault(p => p.RowKey == id);
            var delete = TableOperation.Delete(toBeDeleted);
            Providers.Execute(delete);
        }

        public ProviderEntity Find(string id)
        {
            return Providers.CreateQuery<ProviderEntity>()
                .SingleOrDefault(p => p.PartitionKey == _partitionKey && p.RowKey == id);
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

        public void Update(string id, ProviderEntity provider)
        {
            if (!Providers.CreateQuery<ProviderEntity>().Any(p => p.RowKey == id))
                return;
            var merge = TableOperation.Merge(provider);
            Providers.Execute(merge);
        }

    }
}