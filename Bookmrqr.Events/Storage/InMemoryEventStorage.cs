using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;
using Quintor.CQRS.Events.Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmrqr.Events.Storage
{
    public class InMemoryEventStorage : IEventStorage
    {
        private readonly Dictionary<string, List<IEvent>> _inMemoryDB = new Dictionary<string, List<IEvent>>();
        private readonly IEventAdapter _eventAdapter;

        public InMemoryEventStorage(IEventAdapter eventAdapter)
        {
            _eventAdapter = eventAdapter;
        }

        public IEnumerable<IEvent> GetEvents(string aggregateId, int fromVersion)
        {
            List<IEvent> events;
            _inMemoryDB.TryGetValue(aggregateId, out events);
            return events != null
                ? events.Where(x => x.Version > fromVersion)
                : new List<IEvent>();
        }

        public void Save(IEvent @event)
        {
            List<IEvent> list;
            _inMemoryDB.TryGetValue(@event.AggregateId, out list);
            if (list == null)
            {
                list = new List<IEvent>();
                _inMemoryDB.Add(@event.AggregateId, list);
            }
            list.Add(@event);
            var desEvent = (dynamic)Convert.ChangeType(@event, @event.GetType());
            _eventAdapter.Publish(desEvent);
        }
    }
}
