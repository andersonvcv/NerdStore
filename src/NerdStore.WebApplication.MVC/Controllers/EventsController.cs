using EventSourcing;
using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApplication.MVC.Controllers;

public class EventsController : Controller
{
    private readonly IEventStoreRepository _eventStoreRepository;

    public EventsController(IEventStoreRepository eventStoreRepository)
    {
        _eventStoreRepository = eventStoreRepository;
    }

    [Route("events/{id:guid}")]
    public async Task<IActionResult> Index(Guid id)
    {
        var events = await _eventStoreRepository.GetEvents(id);

        return View(events);
    }
}