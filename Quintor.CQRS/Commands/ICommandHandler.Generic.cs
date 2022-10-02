namespace Quintor.CQRS.Commands
{
    public interface ICommandHandler<TCommand> : ICommandHandler
        where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
