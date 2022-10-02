using Quintor.CQRS.Events;

namespace Bookmrqr.Events
{
    public class AccountDeletedEvent : Event
    {

        public AccountDeletedEvent(string aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
