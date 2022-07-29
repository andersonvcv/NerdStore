namespace NerdStore.Sales.Application.Queries.DTOs;

public class CartDTO
{
    public Guid RequestId { get; set; }
    public Guid ClientId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public decimal Discount { get; set; }
    public string VoucherCode { get; set; }
    public List<CartItemDTO> Items { get; set; }
    public CartPaymentDTO Payment { get; set; }

    public CartDTO()
    {
        Items = new List<CartItemDTO>();
    }
}