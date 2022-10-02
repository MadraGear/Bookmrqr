using Quintor.CQRS.Commands;

namespace Bookmrqr.Domain.Commands
{
    public class CreateBookmarkCommand : Command
    {
        public string UserName { get; set; }
        public string Url { get; internal set; }
        public string Title { get; internal set; }

        public CreateBookmarkCommand(string id, string userName, string url, string title):base(id)
        {
            UserName = userName;
            Url = url;
            Title = title;
        }
    }
}
