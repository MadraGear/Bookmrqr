using Quintor.CQRS.Commands;

namespace Bookmrqr.Domain.Commands
{
    public class CreateAccountCommand : Command
    {
        public string DisplayName { get; internal set; }
        public string Email { get; internal set; }

        public CreateAccountCommand(string userName, string displayName, string email)
            : base(userName)
        {
            DisplayName = displayName;
            Email = email;
        }
    }
}
