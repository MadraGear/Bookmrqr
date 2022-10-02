using System;
using System.Collections.Generic;

namespace Quintor.CQRS.Events.Storage
{
    public interface IEventStorage
    {
        void Save(IEvent @event);
        IEnumerable<IEvent> GetEvents(string aggregateId, int fromVersion);
    }
}
