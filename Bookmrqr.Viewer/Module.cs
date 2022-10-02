using System.Linq;
using Bookmrqr.Events.Adapters;
using Bookmrqr.Events.Handlers;
using Bookmrqr.Events.Storage;
using Bookmrqr.Viewer.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quintor.CQRS.Events;
using Quintor.CQRS.Events.Adapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Bookmrqr.Viewer.Tasks;
using System.IO;

namespace Bookmrqr.Viewer
{
    public static class Module
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            Settings settings = new Settings();
            configuration.Bind(settings);
            services.AddSingleton<ISettings>(settings);

            RegisterEventHandlers(services);
            services.AddTransient<IEventHandlerFactory, EventHandlerFactory>();
            services.AddTransient<IEventStoreConnectionFactory, EventStoreConnectionFactory>();
            services.AddTransient<IEventAdapter, ESEventAdapter>();
            services.AddSingleton<IEventHandlerFactory, EventHandlerFactory>();
            services.AddDbContext<AppDbContext>(options => 
            {
                // string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), 
                //     "BookmrqrData/bookmrqr.db");
                string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "bookmrqr.db");
                options.UseSqlite("Data Source=" + dbPath);
            });
            services.AddSingleton<IAppDbContextFactory, AppDbContextFactory>();
            services.AddSingleton<IHostedService, EventProcessor>();

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