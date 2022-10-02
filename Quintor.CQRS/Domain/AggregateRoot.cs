using Quintor.CQRS.Events;
using Quintor.CQRS.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Quintor.CQRS.Domain
{
    public abstract class AggregateRoot
    {
        private readonly List<IEvent> _changes;

        public string Id { get; set; }
        public int Version { get; set; }
        public int EventVersion { get; protected set; }

        protected AggregateRoot()
        {
            _changes = new List<IEvent>();
        }

        #region IEventProvider

        public IEnumerable<IEvent> GetUncommittedChanges() => _changes;

        public void LoadFromHistory(IEnumerable<IEvent> history)
        {
            foreach (var e in history) 
                ApplyChange(e, false);
            Version = history.Last().Version;
            EventVersion = Version;
        }

        #endregion

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }

        protected void ApplyChange(IEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IEvent @event, bool isNew)
        {
            dynamic d = this;

            d.Handle(Converter.ChangeTo(@event, @event.GetType()));
            if (isNew)
            {
                _changes.Add(@event);
            }
        }
    }
}
