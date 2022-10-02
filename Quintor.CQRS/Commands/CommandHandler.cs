using System;

namespace Quintor.CQRS.Commands
{
    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public Type CommandType => typeof(TCommand);

        public void Execute(ICommand command)
        {
            Execute((TCommand)command);
        }

        public abstract void Execute(TCommand command);
    }
}
