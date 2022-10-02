using System;

namespace Quintor.CQRS.Domain
{
    public interface IEventManager<T> where T : AggregateRoot, new()
    {
        void Save(T aggregate, int? expectedVersion);
        T GetById(string id);
    }
}
