using Bookmrqr.Events;
using Bookmrqr.Viewer.Data;
using Quintor.CQRS.Events;

namespace Bookmrqr.Viewer.EventHandling
{
    public class AccountCreatedEventHandler : EventHandler<AccountAddedEvent>
    {
        private AppDbContext _appDbContext;

        public AccountCreatedEventHandler(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContext = appDbContextFactory.Create();
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
            _appDbContext.Accounts.Add(dto);
            _appDbContext.SaveChanges();
        }
    }
}
