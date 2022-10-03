using NerdStore.Core.Data;

namespace NerdStore.Payment.Business;

public interface IPaymentRepository : IRepository<Payment>
{
    void Add(Payment payment);
    void AddTransaction(Transaction transaction);
}