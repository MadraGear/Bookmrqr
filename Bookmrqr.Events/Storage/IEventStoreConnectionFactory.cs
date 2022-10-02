using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace Bookmrqr.Events.Storage
{
    public interface IEventStoreConnectionFactory
    {
        IEventStoreConnection CreateConnection();

        UserCredentials CreateCredentials();
    }
}
