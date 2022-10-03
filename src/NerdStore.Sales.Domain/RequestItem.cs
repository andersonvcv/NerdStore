using NerdStore.Core.DomainObjects;

namespace NerdStore.Sales.Domain;

public class RequestItem : Entity
{
    public Guid RequestId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; }
    public int Quantity { get; private set; }
    public decimal Value { get; private set; }

    public Request Request { get; private set; }

    public RequestItem(Guid productId, string productName, int quantity, decimal value)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        Value = value;
    }

    protected RequestItem()
    {
        
    }

    internal void AsignRequest(Guid requestId) => RequestId = requestId;
    
    public decimal CalculateValue() => Quantity * Value;
    
    internal void AddQuantity(int value) => Quantity += value;
    
    internal void updateQuantity(int value) => Quantity = value;

    public override bool IsValid()
    {
        return true;
    }
}