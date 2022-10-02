using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Bookmrqr.Events;
using Bookmrqr.FinanceService.Data;
using Microsoft.Extensions.Logging;
using Quintor.CQRS.Events;

namespace Bookmrqr.FinanceService.EventHandling
{
    public class BookmarkAddedEventHandler : EventHandler<BookmarkAddedEvent>
    {
        private readonly ILogger _logger;
        private readonly AppDbContext _appDbContext;
        private readonly ProcessSettings _processSettings;
        private readonly BlockingCollection<BookmarkAddedEvent> _eventQueue = new BlockingCollection<BookmarkAddedEvent>();
        public BookmarkAddedEventHandler(ILogger<BookmarkAddedEventHandler> logger, AppDbContext appDbContext, ProcessSettings processSettings)
        {
            _logger = logger;
            _appDbContext = appDbContext;
            _processSettings = processSettings;
            Task.Run(() => ConsumeEvents(_eventQueue));
        }

        public override void Handle(BookmarkAddedEvent bookmarkAddedEvent)
        {
            _logger.LogInformation("Recieveddddd bookmark added event id={0} url={1}", 
                bookmarkAddedEvent.AggregateId, bookmarkAddedEvent.Url);

            _eventQueue.Add(bookmarkAddedEvent);
        }

        private void ConsumeEvents(BlockingCollection<BookmarkAddedEvent> eventQueue)
        {
            while (!eventQueue.IsCompleted)
            {
                BookmarkAddedEvent nextEvent;
                try
                {
                    if (!eventQueue.TryTake(out nextEvent, Timeout.Infinite))
                    {
                        _logger.LogInformation("Take Blocked");
                    }
                    else
                        _logger.LogInformation("Take event id={0} url={1}", nextEvent.AggregateId, nextEvent.Url);
                }

                catch (System.OperationCanceledException)
                {
                    _logger.LogInformation("Taking canceled.");
                    break;
                }
                try
                {
                    ProcessEvent(nextEvent);
                }
                catch(System.Exception ex)
                {
                    _logger.LogError(0, ex, ex.Message);
                }
            }
        }

        private void ProcessEvent(BookmarkAddedEvent nextEvent)
        {
            ProcessedBookmark processedBookmark = _appDbContext.ProcessedBookmarks
                                .FirstOrDefault(b => b.AggregateId == nextEvent.AggregateId);
            if (processedBookmark == null)
            {
                // processing takes a while
                Thread.Sleep(_processSettings.ProcessingTime);

                processedBookmark = new ProcessedBookmark
                {
                    AggregateId = nextEvent.AggregateId,
                    IsProcessed = true
                };
                _appDbContext.ProcessedBookmarks.Add(processedBookmark);
                _appDbContext.SaveChanges();

                _logger.LogInformation("Processed event id={0} url={1}.", nextEvent.AggregateId, nextEvent.Url);

                SendBookmarkUpdate(nextEvent);
            }
            else
            {
                _logger.LogInformation("Event already processed event id={0} url={1}.", nextEvent.AggregateId, nextEvent.Url);
            }
        }

        private void SendBookmarkUpdate(BookmarkAddedEvent nextEvent)
        {
            HttpClient client = new HttpClient();

            //http://localhost:5000/api/accounts/test/bookmarks/b928f4ec-e1d9-472d-9cb6-8627f497caf9
            client.BaseAddress = new System.Uri("http://localhost:5000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Task<HttpResponseMessage> response = client.PutAsJsonAsync("api/accounts/test/bookmarks/" + nextEvent.AggregateId, new object());
            //response.Wait();
            //response.Result.EnsureSuccessStatusCode();

            _logger.LogInformation("Update bookmark send id={0} url={1}.", nextEvent.AggregateId, nextEvent.Url);
        }
    }
}
