using Bookmrqr.EventProcessor.Data.Databases;
using Bookmrqr.Events;
using Quintor.CQRS.Events;

namespace Bookmrqr.EventProcessor.EventHandling
{
    public class BookmarkDeletedEventHandler : EventHandler<BookmarkDeletedEvent>
    {
        private IReadDatabase _database;

        public BookmarkDeletedEventHandler(IReadDatabase database)
        {
            _database = database;
        }

        public override void Handle(BookmarkDeletedEvent handle)
        {
            _database.DeleteAccount(handle.AggregateId);
        }
    }
}
