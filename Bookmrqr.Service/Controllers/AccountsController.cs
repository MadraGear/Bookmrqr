using Microsoft.AspNetCore.Mvc;
using Quintor.Bookmrqr.Service.Models;
using Bookmrqr.Domain.Commands;
using Quintor.CQRS.Commands;
using Quintor.Bookmrqr.Service.Exceptions.Filters;

namespace Quintor.Bookmrqr.Service.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [BookmrqrApiExceptionFilterAttribute]
    public class AccountsController : Controller
    {
        private readonly ICommandBus _commandBus;

        public AccountsController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        [HttpPost]
        public IActionResult Post([FromBody]Registration registration)
        {
            Account account = new Account()
            {
                DisplayName = registration.DisplayName,
                UserName = registration.UserName,
                Email = registration.Email
            };
            CreateAccountCommand createAccountCommand = new CreateAccountCommand(account.UserName, account.DisplayName, account.Email);
            _commandBus.Send(createAccountCommand);

            return Created(Request.Path + account.UserName, account);
        }

        [HttpDelete("{userName}")]
        public IActionResult Delete(string userName)
        {
            DeleteAccountCommand deleteAccountCommand = new DeleteAccountCommand(userName);
            _commandBus.Send(deleteAccountCommand);

            return Ok();
        }
    }
}
