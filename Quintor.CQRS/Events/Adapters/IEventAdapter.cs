
namespace Quintor.CQRS.Events.Adapters
{
    public interface IEventAdapter
    {
        void Initialize();

        void Publish<T>(T @event) where T : IEvent;
    }
}
