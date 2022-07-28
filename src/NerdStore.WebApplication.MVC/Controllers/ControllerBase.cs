using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.Notifications;

namespace NerdStore.WebApplication.MVC.Controllers;

public class ControllerBase : Controller
{
    protected Guid ClientId = Guid.Parse("8677b3cb-8afc-4aab-9293-7df5dc96f258");

    private readonly DomainNotificationHandler _docDomainNotificationHandler;
    private readonly IMediatoRHandler _mediatoRHandler;

    public ControllerBase(INotificationHandler<DomainNotification> docDomainNotificationHandler, IMediatoRHandler mediatoRHandler)
    {
        _docDomainNotificationHandler = (DomainNotificationHandler)docDomainNotificationHandler;
        _mediatoRHandler = mediatoRHandler;
    }

    protected bool ValidOperation() => !_docDomainNotificationHandler.HasDomainNotifications();

    protected void NotifyError(string code, string message) =>
        _mediatoRHandler.PublishNotification(new DomainNotification(code, message));
}