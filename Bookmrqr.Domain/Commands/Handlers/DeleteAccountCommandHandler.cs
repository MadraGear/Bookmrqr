using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using System;
using Bookmrqr.Domain.Aggregates;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class DeleteAccountCommandHandler : CommandHandler<DeleteAccountCommand>
    {
        private readonly IEventManager<Account> _eventManager;

        public DeleteAccountCommandHandler(IEventManager<Account> eventManager)
        {
            _eventManager = eventManager;
        }

        public override void Execute(DeleteAccountCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var aggregate = _eventManager.GetById(command.Id);
            aggregate.Delete();
            _eventManager.Save(aggregate, aggregate.Version);
        }
    }
}
