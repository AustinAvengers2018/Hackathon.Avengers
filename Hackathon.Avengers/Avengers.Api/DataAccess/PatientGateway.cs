using Avengers.Api.Models;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.DataAccess
{
    public class PatientGateway : IGateway<PatientEntity>
    {
        string _partitionKey = "patient";
        CloudTable Patients;

        public PatientGateway(IAvengersCloudAccess account)
        {
            Patients = account.GetTableFor<PatientEntity>();
        }

        public void Create(PatientEntity newobject)
        {
            var insert = TableOperation.Insert(newobject);
            Patients.Execute(insert);
        }

        public void Delete(string id)
        {
            var toBeDeleted = Patients.CreateQuery<PatientEntity>()
                .FirstOrDefault(p => p.RowKey == id);
            var delete = TableOperation.Delete(toBeDeleted);
           Patients.Execute(delete);
        }

        public PatientEntity Find(string id)
        {
            return Patients.CreateQuery<PatientEntity>()
                .SingleOrDefault(p => p.PartitionKey == _partitionKey && p.RowKey == id);
        }

        public IEnumerable<PatientEntity> Get()
        {
            return Patients.CreateQuery<PatientEntity>()
                .Where(p => p.PartitionKey == _partitionKey);
        }

        public void Update(string id, PatientEntity updated)
        {
            if (!Patients.CreateQuery<PatientEntity>().Any(p => p.RowKey == id))
                return;
            var update = TableOperation.Merge(updated);
            Patients.Execute(update);
        }
    }
}