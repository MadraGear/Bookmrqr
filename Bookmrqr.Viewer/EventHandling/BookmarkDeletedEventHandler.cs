using System.Linq;
using Bookmrqr.Events;
using Bookmrqr.Viewer.Data;
using Quintor.CQRS.Events;

namespace Bookmrqr.Viewer.EventHandling
{
    public class BookmarkDeletedEventHandler : EventHandler<BookmarkDeletedEvent>
    {
        private AppDbContext _appDbContext;

        public BookmarkDeletedEventHandler(IAppDbContextFactory appDbContextFactory)
        {
            _appDbContext = appDbContextFactory.Create();
        }

        public override void Handle(BookmarkDeletedEvent handle)
        {
            Bookmark bookMark = _appDbContext.Bookmarks
                .FirstOrDefault(a => a.Id == handle.AggregateId);

            if (bookMark != null)
            {
                _appDbContext.Bookmarks.Remove(bookMark);
                _appDbContext.SaveChanges();
            }
        }
    }
}
