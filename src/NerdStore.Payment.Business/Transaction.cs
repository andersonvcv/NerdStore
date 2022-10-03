using NerdStore.Core.DomainObjects;

namespace NerdStore.Payment.Business;

public class Transaction : Entity
{
    public Guid RequestId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Total { get; set; }
    public TransactionStatus Status { get; set; }

    public Payment Payment { get; set; }
    
}