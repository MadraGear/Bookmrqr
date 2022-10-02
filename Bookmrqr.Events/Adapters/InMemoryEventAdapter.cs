using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;

namespace Bookmrqr.Events.Adapters
{
    public class InMemoryEventAdapter : IEventAdapter
    {
        private IEventHandlerFactory _eventHandlerFactory;

        public InMemoryEventAdapter(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }

        public void Initialize()
        {
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            foreach (var eventHandler in handlers)
            {
                eventHandler.Handle(@event);
            }
        }
    }
}
