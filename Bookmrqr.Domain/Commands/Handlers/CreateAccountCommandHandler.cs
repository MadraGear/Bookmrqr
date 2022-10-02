using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using System;
using Bookmrqr.Domain.Aggregates;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class CreateAccountCommandHandler : CommandHandler<CreateAccountCommand>
    {
        private readonly IEventManager<Account> _eventManager;

        public CreateAccountCommandHandler(IEventManager<Account> eventManager)
        {
            _eventManager = eventManager;
        }

        public override void Execute(CreateAccountCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Account aggregate = new Account(command.Id, command.DisplayName, command.Email)
            {
                Version = -1
            };

            // TODO validate userName on uniqueness by trying to insert in a key table

            _eventManager.Save(aggregate, aggregate.Version);
        }
    }
}