using Quintor.CQRS.Domain.Exceptions;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Storage;
using Quintor.CQRS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Quintor.CQRS.Domain
{
    public class EventManager<T> : IEventManager<T> where T : AggregateRoot, new()
    {
        private readonly IEventStorage _eventStore;

        public EventManager(IEventStorage eventStore)
        {
            _eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }

        public void Save(T aggregate, int? expectedVersion = null)
        {
            if (expectedVersion != null && _eventStore.GetEvents(aggregate.Id, expectedVersion.Value).Any())
                throw new ConcurrencyException(aggregate.Id);

            int i = 0;
            foreach (IEvent @event in aggregate.GetUncommittedChanges())
            {
                i++;
                @event.Version = aggregate.Version + i;
                @event.TimeStamp = DateTimeOffset.UtcNow;

                _eventStore.Save(@event);
            }
            aggregate.MarkChangesAsCommitted();
        }

        public T GetById(string aggregateId)
        {
            return LoadAggregate(aggregateId);
        }

        private T LoadAggregate(string id)
        {
            T aggregate = new T();

            IEnumerable<IEvent> events = _eventStore.GetEvents(id, -1);
            if (!events.Any())
                throw new AggregateNotFoundException(id);

            aggregate.LoadFromHistory(events);
            return aggregate;
        }
    }
}
