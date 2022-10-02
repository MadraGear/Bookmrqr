using EventStore.ClientAPI;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Storage;
using Quintor.CQRS.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Bookmrqr.Events.Storage
{
    public class ESEventStorage : IEventStorage
    {
        private IEventStoreConnectionFactory _eventStoreConnectionFactory;
        private readonly Microsoft.Extensions.Logging.ILogger _logger;

        public ESEventStorage(IEventStoreConnectionFactory eventStoreConnectionFactory, ILogger<ESEventStorage> logger)
        {
            _eventStoreConnectionFactory = eventStoreConnectionFactory;
            _logger = logger;
        }

        public IEnumerable<IEvent> GetEvents(string aggregateId, int fromVersion)
        {
            string stream = aggregateId;
            fromVersion++;
            List<IEvent> events = new List<IEvent>();

            using (IEventStoreConnection conn = _eventStoreConnectionFactory.CreateConnection())
            {
                StreamEventsSlice result = conn.ReadStreamEventsForwardAsync(stream, fromVersion, 100, true).Result;
                foreach (ResolvedEvent resolvedEvent in result.Events)
                {
                    events.Add(resolvedEvent.Event.Data.ToEvent(resolvedEvent.Event.EventType));
                }                
            }

            return events;
        }

        public void Save(IEvent @event)
        {
            string stream = @event.AggregateId;

            _logger.LogInformation("Saving stream " + stream);

            using (IEventStoreConnection conn = _eventStoreConnectionFactory.CreateConnection())
            {
                conn.AppendToStreamAsync(stream, ExpectedVersion.Any, GetEventDataFor(@event))
                    .Wait();
            }
            _logger.LogInformation("Stream saved " + stream);
        }

        private static EventData GetEventDataFor(IEvent @event)
        {
            return new EventData(Guid.NewGuid(), @event.EventTypeName, true, 
                Encoding.ASCII.GetBytes(@event.ToJson()), null);
        }
    }
}
