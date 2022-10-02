using Bookmrqr.EventProcessor.Data;
using Bookmrqr.EventProcessor.Data.Databases;
using Bookmrqr.Events;
using Quintor.CQRS.Events;

namespace Bookmrqr.EventProcessor.EventHandling
{
    public class BookmarkAddedEventHandler : EventHandler<BookmarkAddedEvent>
    {
        private IReadDatabase _database;

        public BookmarkAddedEventHandler(IReadDatabase database)
        {
            _database = database;
        }

        public override void Handle(BookmarkAddedEvent handle)
        {
            Bookmark dto = new Bookmark()
            {
                Id = handle.AggregateId,
                Version = handle.Version,
                UserName = handle.UserName,
                Url = handle.Url,
                Title=handle.Title
            };
            _database.AddBookmark(dto.UserName, dto);
        }
    }
}
