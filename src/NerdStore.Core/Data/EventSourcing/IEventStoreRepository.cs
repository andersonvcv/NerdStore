using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;

namespace EventSourcing;

public interface IEventStoreRepository
{
    Task Save<TEvent>(TEvent eventStoreEvent) where TEvent : Event;
    Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
}