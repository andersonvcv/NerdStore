using System.Text;
using EventStore.ClientAPI;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;

namespace EventSourcing;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IEventStoreService _eventStoreService;

    public EventStoreRepository(IEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Save<TEvent>(TEvent esEvent) where TEvent : Event
    {
        await _eventStoreService.GetConnection().AppendToStreamAsync(esEvent.AggregateId.ToString(), ExpectedVersion.Any, FormatEvent(esEvent));
    }

    public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
    {
        var eventsPage = await _eventStoreService.GetConnection()
            .ReadStreamEventsBackwardAsync(aggregateId.ToString(), 0, 500, false);

        var storedEvents = new List<StoredEvent>();

        foreach (var resolvedEvent in eventsPage.Events)
        {
            var encodedData = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var jsonData = JsonConvert.DeserializeObject<BaseEvent>(encodedData);

            var storedEvent = new StoredEvent(resolvedEvent.Event.EventId, resolvedEvent.Event.EventType, jsonData.Timestamp,
                encodedData);

            storedEvents.Add(storedEvent);
        }

        return storedEvents.OrderBy(e => e.EntryDate);
    }

    private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent esEvent) where TEvent : Event
    {
        yield return new EventData(Guid.NewGuid(), esEvent.MessageType, true,
            Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(esEvent)), null);
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}