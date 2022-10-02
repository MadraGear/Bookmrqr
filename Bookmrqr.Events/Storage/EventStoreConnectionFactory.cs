using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Bookmrqr.Events.Storage
{
    public class EventStoreConnectionFactory : IEventStoreConnectionFactory
    {
        private static readonly IPAddress DefaultAddress = IPAddress.Loopback;
        private const int DefaultPort = 1113;
        private readonly ISettings _appSettings;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public EventStoreConnectionFactory(ISettings appSettings, ILogger<EventStoreConnectionFactory> logger)
        {
            _appSettings = appSettings;
            _logger = logger;
        }

        public IEventStoreConnection CreateConnection()
        {
            try
            {
                ConnectionSettingsBuilder connectionSettingsBuilder = ConnectionSettings.Create();
                connectionSettingsBuilder.Build();
                string connectionString = string.Format("ConnectTo=tcp://{0}:{1}@{2}:{3};",
                    _appSettings.EventStoreUserName,
                    _appSettings.EventStorePassword,
                    _appSettings.EventStoreAddress,
                    _appSettings.EventStorePort);
                _logger.LogInformation("Connecting to " + connectionString);

                ConnectionSettings settings = ConnectionString.GetConnectionSettings(connectionString);

                IEventStoreConnection connection =  EventStoreConnection.Create(connectionString);

                connection.ConnectAsync().Wait();

                return connection;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Create connection failed");
                throw;
            }
        }

        public UserCredentials CreateCredentials()
        {
            var userName = _appSettings.EventStoreUserName;
            var password = _appSettings.EventStorePassword;

            _logger.LogInformation("With credentials " + userName + ":" + password);

            return new UserCredentials(userName, password);
        }
    }
}
