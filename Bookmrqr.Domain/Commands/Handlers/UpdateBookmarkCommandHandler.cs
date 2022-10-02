using Quintor.CQRS.Commands;
using Quintor.CQRS.Domain;
using System;
using Bookmrqr.Domain.Aggregates;

namespace Bookmrqr.Domain.Commands.Handlers
{
    public class UpdateBoomarkCommandHandler : CommandHandler<UpdateBookmarkCommand>
    {
        private readonly IEventManager<Bookmark> _eventManager;

        public UpdateBoomarkCommandHandler(IEventManager<Bookmark> eventManager)
        {
            _eventManager = eventManager;
        }

        public override void Execute(UpdateBookmarkCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var aggregate = _eventManager.GetById(command.Id);
            aggregate.Update(command.IsProcessed);

            // TODO validate userName on uniqueness by trying to insert in a key table

            _eventManager.Save(aggregate, aggregate.Version);
        }
    }
}