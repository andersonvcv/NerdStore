using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Messages.Notifications;

namespace NerdStore.WebApplication.MVC.Extensions;

public class SummaryViewComponent : ViewComponent
{
    private readonly DomainNotificationHandler _domainNotificationHandler;

    public SummaryViewComponent(INotificationHandler<DomainNotification> domainNotificationHandler)
    {
        _domainNotificationHandler = (DomainNotificationHandler)domainNotificationHandler;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var domainNotifications = await Task.FromResult(_domainNotificationHandler.GetDomainNotifications());
        domainNotifications.ForEach(d => ViewData.ModelState.AddModelError(string.Empty, d.Value));

        return View();
    }
}