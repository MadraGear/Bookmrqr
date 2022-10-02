//using Bookmrqr.Common.Bus;
//using Bookmrqr.Domain.Aggregates;
//using Bookmrqr.Domain.Commands;
//using Bookmrqr.Domain.Commands.Handlers;
//using Quintor.Bookmrqr.Service.Factories;
//using Quintor.CQRS.Commands;
//using Quintor.CQRS.Domain;
//using Quintor.EventStore.EventAdapters;
//using Quintor.EventStore.Events;
//using Quintor.EventStore.EventStorage;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Microsoft.Extensions.DependencyInjection
//{
//    public static class BookmrqrServiceCollectionExtensions
//    {
//        public static void AddBookmrqr(this IServiceCollection services)
//        {
//            services.AddTransient<ICommandHandler<CreateBookmarkCommand>, CreateBookmarkCommandHandler>();
//            services.AddTransient<ICommandHandler<DeleteBookmarkCommand>, DeleteBookmarkCommandHandler>();
//            services.AddTransient<ICommandHandler<CreateAccountCommand>, CreateAccountCommandHandler>();
//            services.AddTransient<ICommandHandler<DeleteAccountCommand>, DeleteAccountCommandHandler>();
//            services.AddTransient<ICommandHandlerFactory, CommandHandlerFactory>();
//            services.AddTransient<ICommandBus, CommandBus>();

//            services.AddTransient<IEventManager<Account>, EventManager<Account>>();
//            services.AddTransient<IEventManager<Bookmark>, EventManager<Bookmark>>();

//            services.AddTransient<IEventHandlerFactory, EventHandlerFactory>();

//            services.AddTransient<IEventStoreConnectionFactory, EventStoreConnectionFactory>();
//            services.AddTransient<IEventStorage, ESEventStorage>();

//            // for read side to get events published by EventStore
//            services.AddSingleton<IEventAdapter, ESEventAdapter>();

//            ServiceProvider serviceProvider = services.BuildServiceProvider();
//            serviceProvider.GetService<IEventAdapter>().Initialize();
//        }
//    }
//}
