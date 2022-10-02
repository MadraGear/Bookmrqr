using System;

namespace Quintor.CQRS.Events
{
    public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
        where TEvent : IEvent
    {
        public Type EventType
        {
            get { return typeof(TEvent); }
        }

        public void Handle(IEvent @event)
        {
            Handle((TEvent)@event);
        }

        public abstract void Handle(TEvent @event);
    }
}
