using Bookmrqr.Events.Storage;
using Bookmrqr.Viewer.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmrqr.Viewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildHost(args).Run();
        }
        
        private static IWebHost BuildHost(string[] args) 
        {
            var host = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        
            using (var scope = host.Services.CreateScope())
            {
                AppDbContext appDbContext = scope.ServiceProvider.GetService<AppDbContext>();
                appDbContext.Database.EnsureCreated();

                var settings = scope.ServiceProvider.GetService<ISettings>();
            }
 
            return host;
        }
    }
}
