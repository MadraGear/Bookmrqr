using Bookmrqr.Events;
using Quintor.CQRS.Domain;
using Quintor.CQRS.Events;

namespace Bookmrqr.Domain.Aggregates
{
    public class Account : AggregateRoot, IHandle<AccountAddedEvent>, IHandle<AccountDeletedEvent>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public Account()
        {

        }

        public Account(string id, string displayName, string email)
        {
            ApplyChange(new AccountAddedEvent(id, displayName, email));
        }

        public void Delete()
        {
            ApplyChange(new AccountDeletedEvent(Id));
        }

        public void Handle(AccountAddedEvent e)
        {
            Id = e.AggregateId;
            DisplayName = e.DisplayName;
            Email = e.Email;
        }

        public void Handle(AccountDeletedEvent e)
        {
        }
    }
}
