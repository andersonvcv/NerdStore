using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApplication.MVC.Controllers;

public class RequestController : ControllerBase
{
    private readonly IRequestQueries _requestQueries;

    public RequestController(INotificationHandler<DomainNotification> docDomainNotificationHandler, IMediatoRHandler mediatoRHandler, IRequestQueries requestQueries) : base(docDomainNotificationHandler, mediatoRHandler)
    {
        _requestQueries = requestQueries;
    }

    [Route("my-requests")]
    public async Task<IActionResult> Index()
    {
        return View(await _requestQueries.GetClientRequests(ClientId));
    }
}