using Quintor.CQRS.Events;

namespace Bookmrqr.Events
{
    public class BookmarkUpdatedEvent : Event
    {
        public string UserName { get; set; }
        public bool IsProcessed { get; set; }
        public BookmarkUpdatedEvent(string userName, string aggregateId, bool isProcessed)
        {
            UserName = userName;
            AggregateId = aggregateId;
            IsProcessed = isProcessed;
        }
    }
}
