using Bookmrqr.Events.Storage;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using EventStore.ClientAPI.SystemData;

namespace Bookmrqr.Events.Adapters
{
    public class ESEventAdapter : IEventAdapter
    {
        //private IEventHandlerFactory _eventHandlerFactory;
        private IEventStoreConnectionFactory _eventStoreConnectionFactory;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private IEventStoreConnection _connection;
        private readonly IEnumerable<string> _eventTypes;
        private ILookup<string, IEventHandler> _lookup;
        private IDictionary<string, Type> _map;

        public ESEventAdapter(IEnumerable<IEventHandler> eventHandlers, IEventStoreConnectionFactory eventStoreConnectionFactory,
            Microsoft.Extensions.Logging.ILogger<ESEventAdapter> logger)
        {
            //_eventHandlerFactory = eventHandlerFactory;
            _eventStoreConnectionFactory = eventStoreConnectionFactory;
            _logger = logger;

            _lookup = eventHandlers.ToLookup(h => h.EventType.Name);
            _map = eventHandlers.ToDictionary(h => h.EventType.Name, h => h.EventType);

            _eventTypes = eventHandlers.Select(h => h.EventType.Name);
        }

        public void Initialize()
        {
            _logger.LogInformation("Initializing");
            _connection = _eventStoreConnectionFactory.CreateConnection();
            UserCredentials credentials = _eventStoreConnectionFactory.CreateCredentials();

            _connection.SubscribeToAllFrom(Position.Start, CatchUpSubscriptionSettings.Default, OnEventAppeared, userCredentials: credentials);
            _logger.LogInformation("Initialized");
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            foreach (var eventHandler in _lookup[@event.GetType().Name])
            {
                eventHandler.Handle(@event);
            }
        }

        private Task OnEventAppeared(EventStoreCatchUpSubscription catchUpSubscription, ResolvedEvent resolvedEvent)
        {
            return Task.Run(() =>
            {
                if (!_eventTypes.Contains(resolvedEvent.Event.EventType))
                    return;

                IEvent @event = DeserializeEvent(resolvedEvent);
                if (@event != null)
                {
                    foreach (var eventHandler in _lookup[@event.GetType().Name])
                    {
                        eventHandler.Handle(@event);
                    }
                }
            });
        }

        private IEvent DeserializeEvent(ResolvedEvent resolvedEvent)
        {
            if (!_lookup.Contains(resolvedEvent.Event.EventType) ||
                !_map.ContainsKey(resolvedEvent.Event.EventType))
            {
                return null;
            }

            Type type = _map[resolvedEvent.Event.EventType];
            string json = Encoding.ASCII.GetString(resolvedEvent.Event.Data);

            return (IEvent)JsonConvert.DeserializeObject(json, type);
        }
    }
}
