using Bookmrqr.EventProcessor.Data;
using Bookmrqr.EventProcessor.Data.Databases;
using Bookmrqr.Events;
using Quintor.CQRS.Events;

namespace Bookmrqr.EventProcessor.EventHandling
{
    public class AccountCreatedEventHandler : EventHandler<AccountAddedEvent>
    {
        private IReadDatabase _database;

        public AccountCreatedEventHandler(IReadDatabase database)
        {
            _database = database;
        }

        public override void Handle(AccountAddedEvent handle)
        {
            Account dto = new Account()
            {
                Id = handle.AggregateId,
                Version = handle.Version,
                DisplayName = handle.DisplayName,
                Email = handle.Email
            };
            _database.AddAccount(dto);
        }
    }
}
