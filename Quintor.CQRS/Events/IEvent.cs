using System;

namespace Quintor.CQRS.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
        string AggregateId { get; set; }
        int Version { get; set; }
        DateTimeOffset TimeStamp { get; set; }
        string EventTypeName { get; }
    }
}
