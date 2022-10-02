
namespace Quintor.CQRS.Events
{
    public interface IHandle<TEvent> where TEvent : IEvent
    {
        void Handle(TEvent e);
    }
}
