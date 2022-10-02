using System.Linq;
using Bookmrqr.Events;
using Bookmrqr.Viewer.Data;
using Quintor.CQRS.Events;

namespace Bookmrqr.Viewer.EventHandling
{
    public class AccountDeletedEventHandler : EventHandler<AccountDeletedEvent>
    {
        private AppDbContext _appDbContext;

        public AccountDeletedEventHandler(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContext = appDbContextFactory.Create();
        }

        public override void Handle(AccountDeletedEvent handle)
        {
            Account account = _appDbContext.Accounts
                .FirstOrDefault(a => a.Id == handle.AggregateId);
            
            if (account != null)
            {
                _appDbContext.Accounts.Remove(account);
                _appDbContext.SaveChanges();
            }
        }
    }
}
