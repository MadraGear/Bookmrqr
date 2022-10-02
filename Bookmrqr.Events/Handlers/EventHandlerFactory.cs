using Quintor.CQRS.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmrqr.Events.Handlers
{
    public class EventHandlerFactory : IEventHandlerFactory
    {
        private readonly ILookup<Type, IEventHandler> _eventHandlers;

        public EventHandlerFactory(IEnumerable<IEventHandler> eventHandlers)
        {
            _eventHandlers = eventHandlers
                .ToLookup(h => h.EventType);
        }

        public IEnumerable<IEventHandler> GetHandlers<T>() where T : IEvent
        {
            return _eventHandlers[typeof(T)];
        }
    }
}
