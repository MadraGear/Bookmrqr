using Quintor.CQRS.Commands;

namespace Bookmrqr.Domain.Commands
{
    public class UpdateBookmarkCommand : Command
    {
        public bool IsProcessed { get; set; }
        public UpdateBookmarkCommand(string id, bool isProcessed)
            : base(id)
        {
            IsProcessed = isProcessed;
        }
    }
}