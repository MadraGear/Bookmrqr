using System.IO;
using System.Linq;
using Bookmrqr.Events.Adapters;
using Bookmrqr.Events.Handlers;
using Bookmrqr.Events.Storage;
using Bookmrqr.FinanceService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;

namespace Bookmrqr.FinanceService
{
    public static class Module
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            Settings settings = new Settings();
            configuration.Bind(settings);
            services.AddSingleton<ISettings>(settings);

            ProcessSettings processSettings = new ProcessSettings();
            configuration.GetSection("ProcessSettings").Bind(processSettings);
            services.AddSingleton<ProcessSettings>(processSettings);

            services.AddSingleton(new LoggerFactory()
                .AddConsole(configuration.GetSection("Logging"))
                .AddDebug());

            RegisterEventHandlers(services);
            services.AddTransient<IEventHandlerFactory, EventHandlerFactory>();
            services.AddTransient<IEventStoreConnectionFactory, EventStoreConnectionFactory>();
            services.AddTransient<IEventAdapter, ESEventAdapter>();
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();

            services.AddDbContext<AppDbContext>(options => 
            {
                // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), 
                //     "BookmrqrData/bookmrqrfinance.db");
                string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "bookmrqrfinance.db");
                options.UseSqlite("Data Source=" + dbPath);
            });

            services.AddTransient<Application>();
        }

        private static void RegisterEventHandlers(IServiceCollection services)
        {
            foreach (var eventHandlerType in typeof(Module).Assembly.GetTypes()
                .Where(t => t.GetInterface(typeof(IEventHandler).Name) != null)
                .Where(t => !t.IsAbstract))
            {
                services.AddTransient(typeof(IEventHandler), eventHandlerType);
            }
        }
    }
}