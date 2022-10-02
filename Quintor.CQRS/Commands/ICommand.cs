using System;

namespace Quintor.CQRS.Commands
{
    public interface ICommand
    {
        string Id { get; }
    }
}
