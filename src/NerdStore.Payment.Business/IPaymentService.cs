using NerdStore.Core.DomainObjects.DTOs;

namespace NerdStore.Payment.Business;

public interface IPaymentService
{
    Task<Transaction> Pay(PaymentRequest paymentRequest);
}