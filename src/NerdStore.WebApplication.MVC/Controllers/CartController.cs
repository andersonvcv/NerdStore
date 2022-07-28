using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApplication.MVC.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly IMediatoRHandler _mediatoRHandler;

        public CartController(INotificationHandler<DomainNotification> domaiNotificationHandler, IProductApplicationService productApplicationService, IMediatoRHandler mediatoRHandler) : base(domaiNotificationHandler, mediatoRHandler)
        {
            _productApplicationService = productApplicationService;
            _mediatoRHandler = mediatoRHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productApplicationService.GetById(id);
            if (product is null) return BadRequest();

            if (product.QuantityInStock < quantity)
            {
                TempData["Error"] = "Insufficient stock";
                return RedirectToAction("ProductDetail", "Display", new { id });
            }

            var command = new AddRequestItemCommand(ClientId, product.Id, product.Name, quantity, product.Value);
            await _mediatoRHandler.SendCommand(command);

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Product not available";
            return RedirectToAction("ProductDetail", "Display", new { id });
        }
    }
}
