namespace Quintor.CQRS.Events
{
    public interface IEventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
