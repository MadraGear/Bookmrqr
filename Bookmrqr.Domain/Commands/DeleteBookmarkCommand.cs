using Quintor.CQRS.Commands;

namespace Bookmrqr.Domain.Commands
{
    public class DeleteBookmarkCommand : Command
    {
        public DeleteBookmarkCommand(string id)
            : base(id)
        {
        }
    }
}
