using Microsoft.WindowsAzure.Storage.Table;

namespace Avengers.Api.DataAccess
{
    public interface IAvengersCloudAccess
    {
        IGateway<T> GetGatewayFor<T>();
        CloudTable GetTableFor<T>();
    }
}