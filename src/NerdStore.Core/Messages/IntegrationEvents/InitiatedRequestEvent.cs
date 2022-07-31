using NerdStore.Core.DomainObjects.DTOs;

namespace NerdStore.Core.Messages.IntegrationEvents;

public class InitiatedRequestEvent : IntegrationEvent
{
    public Guid RequestId { get; private set; }
    public Guid ClientId { get; private set; }
    public decimal Total { get; private set; }
    public RequestItems RequestItems { get; private set; }
    public string CardName { get; private set; }
    public string CardNumber { get; private set; }
    public string CardExpirationDate { get; private set; }
    public string CardCVV { get; private set; }

    public InitiatedRequestEvent(Guid requestId, Guid clientId, RequestItems items, decimal total, string cardName, string cardNumber, string cardExpirationDate, string cardCvv)
    {
        RequestId = requestId;
        ClientId = clientId;
        RequestItems = items;
        Total = total;
        CardName = cardName;
        CardNumber = cardNumber;
        CardExpirationDate = cardExpirationDate;
        CardCVV = cardCvv;
    }
}