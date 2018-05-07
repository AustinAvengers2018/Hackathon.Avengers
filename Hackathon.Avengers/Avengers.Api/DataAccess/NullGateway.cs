using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Avengers.Api.DataAccess
{
    public class NullGateway : IGateway<object>
    {
        private AvengersCloudAccess avengersCloudAccess;

        public void Create(object newobject)
        {
        }

        public void Delete(int id)
        {
        }

        public object Find(int id)
        {
            return null;
        }

        public IEnumerable<object> Get()
        {
            return new List<object>();
        }

        public void Update(int id, object updated)
        {
        }
    }
}