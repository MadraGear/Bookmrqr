
namespace Quintor.CQRS.Events
{
    using System;

    public interface IEventHandler
    {
        Type EventType { get; }

        void Handle(IEvent @event);
    }
}
