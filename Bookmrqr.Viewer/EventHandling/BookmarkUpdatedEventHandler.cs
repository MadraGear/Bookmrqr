using System.Linq;
using Bookmrqr.Events;
using Bookmrqr.Viewer.Data;
using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events;

namespace Bookmrqr.Viewer.EventHandling
{
    public class BookmarkUpdatedEventHandler : EventHandler<BookmarkUpdatedEvent>
    {
        private readonly ILogger _logger;
        private AppDbContext _appDbContext;

        public BookmarkUpdatedEventHandler(IAppDbContextFactory appDbContextFactory, ILogger<BookmarkUpdatedEventHandler> logger)
        {
            _appDbContext = appDbContextFactory.Create();
            _logger = logger;
        }

        public override void Handle(BookmarkUpdatedEvent handle)
        {
            Bookmark bookMark = _appDbContext.Bookmarks
                .FirstOrDefault(a => a.Id == handle.AggregateId);

            if (bookMark != null)
            {
                _logger.LogInformation("Recieved bookmark update event: id={0} user={1} url={2}",
                    bookMark.Id, bookMark.UserName, bookMark.Url);
                bookMark.IsProcessed = handle.IsProcessed;
                _appDbContext.Bookmarks.Update(bookMark);
                _appDbContext.SaveChanges();
            }
        }
    }
}
