using Quintor.CQRS.Commands;
using System;

namespace Quintor.CQRS.Commands
{
    public abstract class Command : ICommand
    {
        public string Id { get; private set; }
        public Command(string id)
        {
            Id = id;
        }
    }
}
