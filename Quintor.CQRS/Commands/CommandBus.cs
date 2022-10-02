using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Quintor.CQRS.Commands;
using Microsoft.Extensions.Logging;

namespace Bookmrqr.Common.Bus
{
    public class CommandBus : ICommandBus
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly ILogger _logger;
        private readonly BlockingCollection<Action> _commandQueue = new BlockingCollection<Action>();

        public CommandBus(ICommandHandlerFactory commandHandlerFactory, ILogger<CommandBus> logger)
        {
            _logger = logger;
            _commandHandlerFactory = commandHandlerFactory;
            Task.Run(() => ConsumeEvents(_commandQueue));
        }

        public void Send<T>(T command) where T : ICommand
        {
            _commandQueue.Add(()=>
            {
                ICommandHandler handler = _commandHandlerFactory.GetHandler<T>();
                handler.Execute(command);
            });
            
        }

        private void ConsumeEvents(BlockingCollection<Action> commandQueue)
        {
            while (!commandQueue.IsCompleted)
            {
                Action commandAction;
                try
                {
                    if (commandQueue.TryTake(out commandAction, Timeout.Infinite))
                    {
                        commandAction();
                    }
                    
                }
                catch (System.OperationCanceledException)
                {
                    _logger.LogInformation("ConsumeEvents canceled.");
                    break;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Exception during consuming events.");
                    break;
                }
            }
        }
    }
}
