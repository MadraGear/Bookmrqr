using System.Linq;
using Bookmrqr.Events;
using Bookmrqr.Viewer.Data;
using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events;

namespace Bookmrqr.Viewer.EventHandling
{
    public class BookmarkAddedEventHandler : EventHandler<BookmarkAddedEvent>
    {
        private readonly ILogger _logger;
        private AppDbContext _appDbContext;

        public BookmarkAddedEventHandler(IAppDbContextFactory appDbContextFactory, ILogger<BookmarkAddedEventHandler> logger)
        {
            _logger = logger;
            _appDbContext = appDbContextFactory.Create();
        }

        public override void Handle(BookmarkAddedEvent handle)
        {
            try
            {
                Bookmark bookMark = _appDbContext.Bookmarks
                    .FirstOrDefault(a => a.Id == handle.AggregateId);

                if (bookMark == null)
                {
                    bookMark = new Bookmark()
                    {
                        Id = handle.AggregateId,
                        Version = handle.Version,
                        UserName = handle.UserName,
                        Url = handle.Url,
                        Title=handle.Title,
                        Timestamp=handle.TimeStamp.DateTime
                    };
                    _logger.LogInformation("Recieved bookmark added event: id={0} user={1} url={2}",
                        bookMark.Id, bookMark.UserName, bookMark.Url);
                    _appDbContext.Bookmarks.Add(bookMark);
                    _appDbContext.SaveChanges();
                }
            }
            catch(System.Exception ex)
            {
                _logger.LogError(0, ex, ex.Message);
            }
        }
    }
}
