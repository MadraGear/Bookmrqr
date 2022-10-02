using System;

namespace Quintor.CQRS.Events
{
    public abstract class Event : IEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public virtual string EventTypeName
        {
            get
            {
                return GetType().Name;
            }
        }

        public Event()
        {
            Id = Guid.NewGuid();
        }
    }
}
