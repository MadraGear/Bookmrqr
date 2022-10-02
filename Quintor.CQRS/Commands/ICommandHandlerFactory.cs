namespace Quintor.CQRS.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetHandler<T>() where T : ICommand;
    }
}
