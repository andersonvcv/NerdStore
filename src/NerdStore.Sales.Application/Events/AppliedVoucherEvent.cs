using NerdStore.Core.Messages;

namespace NerdStore.Sales.Application.Events;

public class AppliedVoucherEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid RequestId { get; private set; }
    public Guid VoucherId { get; private set; }

    public AppliedVoucherEvent(Guid clientId, Guid requestId, Guid voucherId)
    {
        ClientId = clientId;
        RequestId = requestId;
        VoucherId = voucherId;
    }
}