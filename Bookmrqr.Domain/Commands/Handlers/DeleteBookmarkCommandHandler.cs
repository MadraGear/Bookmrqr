using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using System;
using Bookmrqr.Domain.Aggregates;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class DeleteBookmarkCommandHandler : CommandHandler<DeleteBookmarkCommand>
    {
        private readonly IEventManager<Bookmark> _eventManager;

        public DeleteBookmarkCommandHandler(IEventManager<Bookmark> eventManager)
        {
            _eventManager = eventManager;
        }

        public override void Execute(DeleteBookmarkCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var aggregate = _eventManager.GetById(command.Id);
            aggregate.Delete();
            _eventManager.Save(aggregate, aggregate.Version);
        }
    }
}
