using System.Collections.Generic;

namespace Avengers.Api.DataAccess
{
    public interface IGateway<T>
    {
        IEnumerable<T> Get();
        T Find(string id);
        void Create(T newobject);
        void Update(string id, T updated);
        void Delete(string id);
    }
}