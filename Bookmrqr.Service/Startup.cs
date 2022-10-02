using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bookmrqr.Events.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quintor.Bookmrqr.Service.Ioc;
using Quintor.Bookmrqr.Service.Models;
using System;

namespace Quintor.Bookmrqr.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Adds services required for using options.
            services.AddOptions();

            // Register the IConfiguration instance which MyOptions binds against.
            services.Configure<Settings>(Configuration);
            services.AddMvc();

            services.AddSingleton(new LoggerFactory()
                .AddConsole(Configuration.GetSection("Logging"))
                .AddDebug());

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<BookmrqrAutofacModule>();
            containerBuilder.Populate(services);
            IContainer container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
