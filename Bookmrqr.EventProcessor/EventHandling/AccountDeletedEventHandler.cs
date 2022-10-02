using Bookmrqr.EventProcessor.Data.Databases;
using Bookmrqr.Events;
using Quintor.CQRS.Events;

namespace Bookmrqr.EventProcessor.EventHandling
{
    public class AccountDeletedEventHandler : EventHandler<AccountDeletedEvent>
    {
        private IReadDatabase _database;

        public AccountDeletedEventHandler(IReadDatabase database)
        {
            _database = database;
        }

        public override void Handle(AccountDeletedEvent handle)
        {
            _database.DeleteAccount(handle.AggregateId);
        }
    }
}
