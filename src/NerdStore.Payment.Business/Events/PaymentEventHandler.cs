using MediatR;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages.IntegrationEvents;

namespace NerdStore.Payment.Business.Events;

public class PaymentEventHandler : INotificationHandler<ConfirmedRequestEvent>
{
    private readonly IPaymentService _paymentService;

    public PaymentEventHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task Handle(ConfirmedRequestEvent integrarionEvent, CancellationToken cancellationToken)
    {
        var paymentRequest = new PaymentRequest
        {
            RequestId = integrarionEvent.RequestId,
            ClientId = integrarionEvent.ClientId,
            Total = integrarionEvent.Total,
            CardName = integrarionEvent.CardName,
            CardNumber = integrarionEvent.CardNumber,
            CardExpirationDate = integrarionEvent.CardExpirationDate,
            CardCVV = integrarionEvent.CardCVV,
        };

        await _paymentService.Pay(paymentRequest);
    }
}