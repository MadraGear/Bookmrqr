using Quintor.CQRS.Events;

namespace Bookmrqr.Events
{
    public class AccountAddedEvent : Event
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public AccountAddedEvent(string aggregateId, string displayName, string email)
        {
            AggregateId = aggregateId;
            DisplayName = displayName;
            Email = email;
        }
    }
}
