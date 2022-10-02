using Quintor.CQRS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly Dictionary<Type, ICommandHandler> _commandHandlers;

        public CommandHandlerFactory(IEnumerable<ICommandHandler> commandHandlers)
        {
            _commandHandlers = commandHandlers
                .ToDictionary(h => h.CommandType, h => h);
        }

        public ICommandHandler GetHandler<T>() where T : ICommand
        {
            if (!_commandHandlers.TryGetValue(typeof(T), out ICommandHandler commandHandler))
                throw new InvalidOperationException("No commandhandler found for command " + typeof(T).ToString());

            return commandHandler;
        }
    }
}
