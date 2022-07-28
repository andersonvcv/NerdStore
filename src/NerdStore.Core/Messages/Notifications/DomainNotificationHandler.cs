using MediatR;

namespace NerdStore.Core.Messages.Notifications;

public class DomainNotificationHandler : INotificationHandler<DomainNotification>
{
    private List<DomainNotification> _domainNotifications;

    public DomainNotificationHandler()
    {
        _domainNotifications = new List<DomainNotification>();
    }


    public async Task Handle(DomainNotification notification, CancellationToken cancellationToken)
    {
        _domainNotifications.Add(notification);
    }

    public virtual List<DomainNotification> GetDomainNotifications() => _domainNotifications;

    public virtual bool HasDomainNotifications() => _domainNotifications.Any();

    public void Dispose()
    {
        _domainNotifications = new List<DomainNotification>();
    }
}