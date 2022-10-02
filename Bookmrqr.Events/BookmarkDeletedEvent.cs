using Quintor.CQRS.Events;

namespace Bookmrqr.Events
{
    public class BookmarkDeletedEvent : Event
    {
        public string UserName { get; set; }
        public BookmarkDeletedEvent(string userName, string aggregateId)
        {
            UserName = userName;
            AggregateId = aggregateId;
        }
    }
}
