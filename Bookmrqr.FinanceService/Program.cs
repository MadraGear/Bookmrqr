using System;
using System.IO;
using Bookmrqr.Events.Storage;
using Bookmrqr.FinanceService.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmrqr.FinanceService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            // Create a service collection and configure our depdencies
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices(configuration);
        
            // Build the our IServiceProvider and set our static reference to it
            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            AppDbContext appDbContext = serviceProvider.GetService<AppDbContext>();
            appDbContext.Database.EnsureCreated();
        
            // Enter the applicaiton.. (run!)
            serviceProvider.GetService<Application>().Run();
        }
    }
}
