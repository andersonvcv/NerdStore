namespace NerdStore.Core.Data.EventSourcing;

public class StoredEvent
{
    public Guid Id { get; private set; }
    public string Type { get; private set; }
    public DateTime EntryDate { get; private set; }
    public string Data { get; private set; }

    public StoredEvent(Guid id, string type, DateTime entryDate, string data)
    {
        Id = id;
        Type = type;
        EntryDate = entryDate;
        Data = data;
    }
}