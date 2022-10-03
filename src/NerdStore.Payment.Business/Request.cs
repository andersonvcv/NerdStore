namespace NerdStore.Payment.Business;

public class Request
{
    public Guid Id { get; set; }
    public decimal Value { get; set; }
    public List<Product> Products { get; set; }
}