using NerdStore.Core.Data;
using NerdStore.Payment.Business;

namespace NerdStore.Payment.Data.Repository;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentContext _paymentContext;

    public PaymentRepository(PaymentContext paymentContext)
    {
        _paymentContext = paymentContext;
    }

    public IUnitOfWork UnitOfWork => _paymentContext;

    public void Add(Business.Payment payment)
    {
        _paymentContext.Payment.Add(payment);
    }

    public void AddTransaction(Transaction transaction)
    {
        _paymentContext.Transaction.Add(transaction);
    }

    public void Dispose()
    {
        _paymentContext.Dispose();
    }
}