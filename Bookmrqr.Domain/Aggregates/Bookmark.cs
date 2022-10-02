using Quintor.CQRS.Domain;
using Bookmrqr.Events;
using Quintor.CQRS.Events;

namespace Bookmrqr.Domain.Aggregates
{
    public class Bookmark : AggregateRoot, IHandle<BookmarkAddedEvent>, IHandle<BookmarkDeletedEvent>, IHandle<BookmarkUpdatedEvent>
    {
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }

        public Bookmark()
        {

        }

        public Bookmark(string id, string userName, string url, string title)
        {
            ApplyChange(new BookmarkAddedEvent(id, userName, url, title));
        }

        public void Update(bool isProcessed)
        {
            ApplyChange(new BookmarkUpdatedEvent(UserName, Id, isProcessed));
        }

        public void Delete()
        {
            ApplyChange(new BookmarkDeletedEvent(UserName, Id));
        }

        public void Handle(BookmarkAddedEvent e)
        {
            Id = e.AggregateId;
            UserName = e.UserName;
            Url = e.Url;
            Title = e.Title;
        }

        public void Handle(BookmarkUpdatedEvent e)
        {
            IsProcessed = e.IsProcessed;
        }

        public void Handle(BookmarkDeletedEvent e)
        {
        }
    }
}
