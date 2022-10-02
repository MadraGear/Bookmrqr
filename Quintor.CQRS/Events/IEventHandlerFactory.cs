using System.Collections.Generic;

namespace Quintor.CQRS.Events
{
    public interface IEventHandlerFactory
    {
        IEnumerable<IEventHandler> GetHandlers<T>() where T : IEvent;
    }
}
