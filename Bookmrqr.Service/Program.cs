using Bookmrqr.Events.Storage;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quintor.Bookmrqr.Service.Controllers;

namespace Quintor.Bookmrqr.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        private static IWebHost BuildHost(string[] args) 
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .Build();
        
            using (var scope = host.Services.CreateScope())
            {
                var settings = scope.ServiceProvider.GetService<ISettings>();
                var logger = scope.ServiceProvider.GetService<ILogger<BookmarksController>>();
            }
 
            return host;
        }
    }
}
