namespace NerdStore.Sales.Application.Queries.DTOs;

public class CartItemDTO
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Value { get; set; }
    public decimal Total { get; set; }
}