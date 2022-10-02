using Bookmrqr.Domain.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Quintor.Bookmrqr.Service.Exceptions.Filters;
using Quintor.Bookmrqr.Service.Models;
using Quintor.CQRS.Commands;
using System;

namespace Quintor.Bookmrqr.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/accounts/")]
    [BookmrqrApiExceptionFilterAttribute]
    public class BookmarksController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger _logger;

        public BookmarksController(ICommandBus commandBus, ILogger<BookmarksController> logger)
        {
            _logger = logger;
            _commandBus = commandBus;
        }

        [HttpPost("{userName}/bookmarks")]
        public IActionResult Post(string userName, [FromBody]Bookmark bookmark)
        {
            bookmark.Id = Guid.NewGuid().ToString();
            CreateBookmarkCommand createCommand = new CreateBookmarkCommand(bookmark.Id, userName, bookmark.Url, bookmark.Title);

            _logger.LogInformation("Sending bookmark create command: id={0} url={1}",
                bookmark.Id, createCommand.Url);
            _commandBus.Send(createCommand);

            return Created(Request.Path + bookmark.Id, bookmark);
        }

        [HttpPut("{userName}/bookmarks/{bookmarkId}")]
        public IActionResult Put(string userName, string bookmarkId)
        {
            UpdateBookmarkCommand updateCommand = new UpdateBookmarkCommand(bookmarkId, true);
            _logger.LogInformation("Sending bookmark update command: user:{0} id:{1}", userName, bookmarkId);
            _commandBus.Send(updateCommand);

            return Ok();
        }

        [HttpDelete("{userName}/bookmarks/{bookmarkId}")]
        public IActionResult Delete(string userName, string bookmarkId)
        {
            DeleteBookmarkCommand deleteCommand = new DeleteBookmarkCommand(bookmarkId);
            _logger.LogInformation("Sending bookmark delete command: user:{0} id:(1)", userName, bookmarkId);
            _commandBus.Send(deleteCommand);

            return Ok();
        }
    }
}