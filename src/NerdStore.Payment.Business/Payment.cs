using NerdStore.Core.DomainObjects;

namespace NerdStore.Payment.Business;

public class Payment : Entity, IAggregateRoot
{
    public Guid RequestId { get; set; }
    public string Status { get; set; }
    public decimal Value { get; set; }

    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CardExiprationDate { get; set; }
    public string CardCVV { get; set; }
    
    public Transaction Transaction { get; set; }
    
}