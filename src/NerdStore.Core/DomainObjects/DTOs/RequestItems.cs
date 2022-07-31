namespace NerdStore.Core.DomainObjects.DTOs;

public class RequestItems
{
    public Guid RequestId { get; set; }
    public ICollection<Item> Items { get; set; }
}

public class Item
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}