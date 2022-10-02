using Autofac;
using Bookmrqr.EventProcessor.Data.Databases;
using Bookmrqr.Events.Adapters;
using Bookmrqr.Events.Handlers;
using Bookmrqr.Events.Storage;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;
using System.Linq;

namespace Bookmrqr.EventProcessor.Ioc
{
    public class BookmrqrAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => c.Resolve<IOptions<Settings>>().Value).As<ISettings>();

            RegisterEventHandlers(builder);
            // for read side to get events published by EventStore
            builder.RegisterType<EventHandlerFactory>().As<IEventHandlerFactory>();
            builder.RegisterType<EventStoreConnectionFactory>().As<IEventStoreConnectionFactory>();
            builder.RegisterType<ESEventAdapter>().As<IEventAdapter>().SingleInstance();

            builder.RegisterType<InMemoryReadDatabase>().As<IReadDatabase>().SingleInstance();
        }

        private void RegisterEventHandlers(ContainerBuilder builder)
        {
            foreach (var eventHandlerType in GetType().Assembly.GetTypes()
                .Where(t => t.GetInterface(typeof(IEventHandler).Name) != null)
                .Where(t => !t.IsAbstract))
            {
                builder.RegisterType(eventHandlerType).As(typeof(IEventHandler));
            }
        }
    }
}
