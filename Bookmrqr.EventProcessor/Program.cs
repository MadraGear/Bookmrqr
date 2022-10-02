using Autofac;
using Bookmrqr.EventProcessor.Ioc;
using Bookmrqr.Events.Storage;
using Microsoft.Extensions.Configuration;
using Quintor.CQRS.Events.Adapters;
using System;
using System.IO;

namespace Bookmrqr.EventProcessor
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();

            Settings settings = new Settings();
            configuration.Bind(settings);

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<BookmrqrAutofacModule>();
            containerBuilder.RegisterInstance(settings).As<ISettings>();
            using (var container = containerBuilder.Build())
            {
                container.Resolve<IEventAdapter>().Initialize();
                Console.ReadLine();
            }
        }
    }
}
