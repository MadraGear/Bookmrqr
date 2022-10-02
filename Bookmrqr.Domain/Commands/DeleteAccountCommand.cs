using Quintor.CQRS.Commands;

namespace Bookmrqr.Domain.Commands
{
    public class DeleteAccountCommand : Command
    {
        public DeleteAccountCommand(string id)
            : base(id)
        {
        }
    }
}
