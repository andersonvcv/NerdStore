using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class AddedRequestItemEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public decimal Value { get; private set; }
    public int Quantity { get; private set; }

    public AddedRequestItemEvent(Guid clientId, Guid requestId, Guid productId, string productName, decimal value, int quantity)
    {
        AggregateId = requestId;
        ClientId = clientId;
        RequestId = requestId;
        ProductId = productId;
        ProductName = productName;
        Value = value;
        Quantity = quantity;
    }
}