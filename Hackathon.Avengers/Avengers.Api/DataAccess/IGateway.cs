using System.Collections.Generic;

namespace Avengers.Api.DataAccess
{
    public interface IGateway<T>
    {
        IEnumerable<T> Get();
        T Find(int id);
        void Create(T newobject);
        void Update(int id, T updated);
        void Delete(int id);
    }
}