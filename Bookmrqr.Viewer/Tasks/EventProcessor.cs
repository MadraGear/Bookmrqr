using System;
using System.Threading;
using System.Threading.Tasks;
using Bookmrqr.Events.Storage;
using Bookmrqr.Viewer.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events.Adapters;

namespace Bookmrqr.Viewer.Tasks
{
    public class EventProcessor : BackgroundService
    {
        private readonly IEventAdapter _eventAdapter;
        private readonly ILogger _logger;

        public EventProcessor(IEventAdapter eventAdapter, ILogger<EventProcessor> logger)
        {
            _logger = logger;
            _eventAdapter = eventAdapter;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Strart event processing");

                _eventAdapter.Initialize();

                stoppingToken.WaitHandle.WaitOne();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, "Stopped processing events");
            }
            return Task.CompletedTask;
        }
    }
}