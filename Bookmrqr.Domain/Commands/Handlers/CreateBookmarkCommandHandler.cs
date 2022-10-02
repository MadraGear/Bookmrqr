using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using System;
using Bookmrqr.Domain.Aggregates;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class CreateBookmarkCommandHandler : CommandHandler<CreateBookmarkCommand>
    {
        private readonly IEventManager<Bookmark> _eventManager;

        public CreateBookmarkCommandHandler(IEventManager<Bookmark> eventManager)
        {
            _eventManager = eventManager;
        }

        public override void Execute(CreateBookmarkCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            Bookmark aggregate = new Bookmark(command.Id, command.UserName, command.Url, command.Title)
            {
                Version = -1
            };

            // TODO validate userName on uniqueness by trying to insert in a key table

            _eventManager.Save(aggregate, aggregate.Version);
        }
    }
}