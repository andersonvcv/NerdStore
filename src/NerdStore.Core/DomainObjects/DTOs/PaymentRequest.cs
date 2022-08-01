namespace NerdStore.Core.DomainObjects.DTOs;

public class PaymentRequest
{
    public Guid RequestId { get; set; }
    public Guid ClientId { get; set; }
    public decimal Total { get; set; }
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CardExpirationDate { get; set; }
    public string CardCVV { get; set; }
}