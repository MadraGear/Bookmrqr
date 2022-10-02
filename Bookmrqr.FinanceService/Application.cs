using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events.Adapters;

namespace Bookmrqr.FinanceService
{
    public class Application
    {
        private readonly ILogger _logger;
        private readonly IEventAdapter _eventAdapter;
        public Application(IEventAdapter eventAdapter, ILogger<Application> logger)
        {
            _logger = logger;
            _eventAdapter = eventAdapter;
        }
        public void Run()
        {
            _logger.LogInformation("Running FinanceService...");

            _eventAdapter.Initialize();

            System.Console.ReadLine();
        }
    }
}