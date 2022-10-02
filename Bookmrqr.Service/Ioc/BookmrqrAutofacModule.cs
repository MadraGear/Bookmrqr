using Autofac;
using Bookmrqr.Common.Bus;
using Bookmrqr.Domain.Aggregates;
using Bookmrqr.Domain.Commands.Handlers;
using Bookmrqr.Events.Storage;
using Microsoft.Extensions.Options;
using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using Quintor.CQRS.Events.Storage;
using System;
using System.Linq;

namespace Quintor.Bookmrqr.Service.Ioc
{
    public class BookmrqrAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CreateBookmarkCommandHandler>().As<ICommandHandler>();
            //builder.RegisterType<DeleteBookmarkCommandHandler>().As<ICommandHandler>();
            //builder.RegisterType<CreateAccountCommandHandler>().As<ICommandHandler>();
            //builder.RegisterType<DeleteAccountCommandHandler>().As<ICommandHandler>();
            RegisterEventHandlers(builder);

            builder.RegisterType<CommandHandlerFactory>().As<ICommandHandlerFactory>();
            builder.RegisterType<CommandBus>().As<ICommandBus>();

            builder.RegisterType<EventManager<Account>>().As<IEventManager<Account>>();
            builder.RegisterType<EventManager<Bookmark>>().As<IEventManager<Bookmark>>();

            builder.RegisterType<ESEventStorage>().As<IEventStorage>();
            builder.RegisterType<EventStoreConnectionFactory>().As<IEventStoreConnectionFactory>();

            builder.Register(c => c.Resolve<IOptions<Settings>>().Value).As<ISettings>();

            // for read side to get events published by EventStore
            //builder.RegisterType<EventHandlerFactory>().As<IEventHandlerFactory>();
            //builder.RegisterType<EventStoreConnectionFactory>().As<IEventStoreConnectionFactory>();
            //builder.RegisterType<ESEventAdapter>().As<IEventAdapter>().SingleInstance();
        }

        private void RegisterEventHandlers(ContainerBuilder builder)
        {
            foreach (Type commandHandlerType in typeof(CommandHandlerFactory).Assembly.GetTypes()
                .Where(t => t.GetInterface(typeof(ICommandHandler).Name) != null)
                .Where(t => !t.IsAbstract))
            {
                builder.RegisterType(commandHandlerType).As(typeof(ICommandHandler));
            }
        }
    }
}
