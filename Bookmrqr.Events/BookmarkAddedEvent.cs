using Quintor.CQRS.Events;

namespace Bookmrqr.Events
{
    public class BookmarkAddedEvent : Event
    {
        public string UserName { get; set; }
        public string Url { get; internal set; }
        public string Title { get; internal set; }

        public BookmarkAddedEvent(string aggregateId, string userName, string url,
            string title)
        {
            AggregateId = aggregateId;
            UserName = userName;
            Url = url;
            Title = title;
        }
    }
}
