using NerdStore.Payment.Business;

namespace NerdStore.Payment.AntiCorruption;

public class PaymentCreditCardFacade : IPaymentCreditCardFacade
{
    private readonly IPaypalGateway _paypalGateway;
    private readonly IConfigurationManager _configurationManager;

    public PaymentCreditCardFacade(IPaypalGateway paypalGateway, IConfigurationManager configurationManager)
    {
        _paypalGateway = paypalGateway;
        _configurationManager = configurationManager;
    }

    public Transaction Pay(Request request, Business.Payment payment)
    {
        var apiKey = _configurationManager.GetValue("apiKey");
        var encryptionKey = _configurationManager.GetValue("encryptionKey");

        var serviceKey = _paypalGateway.GetPaypalServiceKey(apiKey, encryptionKey);
        var cardHashKey = _paypalGateway.GetPaypalServiceKey(serviceKey, payment.CardNumber);

        var paymentResult = _paypalGateway.CommitTransaction(cardHashKey, request.Id.ToString(), payment.Value);

        var transaction = new Transaction
        {
            RequestId = request.Id,
            Total = request.Value,
            PaymentId = payment.Id,
        };

        if (paymentResult)
        {
            transaction.Status = TransactionStatus.Paid;
            return transaction;
        }

        transaction.Status = TransactionStatus.Rejected;
        return transaction;
    }
}