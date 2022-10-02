using System;

namespace Quintor.CQRS.Commands
{
    public interface ICommandHandler
    {
        Type CommandType { get; }
        void Execute(ICommand command);
    }
}
