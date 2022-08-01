using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.Notifications;

namespace NerdStore.Payment.Business;

public class PaymentService : IPaymentService
{
    private readonly IMediatoRHandler _mediatoRHandler;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentCreditCardFacade _paymentCreditCardFacade;

    public PaymentService(IMediatoRHandler mediatoRHandler, IPaymentRepository paymentRepository, IPaymentCreditCardFacade paymentCreditCardFacade)
    {
        _mediatoRHandler = mediatoRHandler;
        _paymentRepository = paymentRepository;
        _paymentCreditCardFacade = paymentCreditCardFacade;
    }

    public async Task<Transaction> Pay(PaymentRequest paymentRequest)
    {
        var request = new Request
        {
            Id = paymentRequest.RequestId,
            Value = paymentRequest.Total
        };

        var payment = new Payment
        {
            Value = paymentRequest.Total,
            CardName = paymentRequest.CardName,
            CardNumber = paymentRequest.CardNumber,
            CardExiprationDate = paymentRequest.CardExpirationDate,
            CardCVV = paymentRequest.CardCVV,
            RequestId = paymentRequest.RequestId
        };

        var transaction = _paymentCreditCardFacade.Pay(request, payment);

        if (transaction.Status == TransactionStatus.Paid)
        {
            payment.AddEvent(new CompletedPaymentEvent(request.Id, paymentRequest.ClientId, transaction.PaymentId, transaction.Id, request.Value));

            _paymentRepository.Add(payment);
            _paymentRepository.AddTransaction(transaction);

            await _paymentRepository.UnitOfWork.Commit();
            return transaction;
        }

        await _mediatoRHandler.PublishNotification(new DomainNotification("Payment", "Payment rejected"));
        await _mediatoRHandler.PublishEvent(new PaymentRejectedEvent(request.Id, paymentRequest.ClientId, transaction.PaymentId, transaction.Id, request.Value));

        return transaction;
    }
}