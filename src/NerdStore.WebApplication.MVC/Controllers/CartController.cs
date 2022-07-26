using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.MediatR;
using NerdStore.Sales.Application.Commands;

namespace NerdStore.WebApplication.MVC.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly IMediatRHandler _mediatRHandler;

        public CartController(IProductApplicationService productApplicationService, IMediatRHandler mediatRHandler)
        {
            _productApplicationService = productApplicationService;
            _mediatRHandler = mediatRHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid productId, int quantity)
        {
            var product = await _productApplicationService.GetById(productId);
            if (product is null) return BadRequest();

            if (product.QuantityInStock < quantity)
            {
                TempData["Error"] = "Insufficient stock";
                return RedirectToAction("ProductDetail", "Display", new { productId });
            }

            var command = new AddRequestItemCommand(ClientId, product.Id, product.Name, quantity, product.Value);
            await _mediatRHandler.PublishCommand(command);

            TempData["Error"] = "Product not available";
            return RedirectToAction("ProductDetail", "Display", new { productId });
        }
    }
}
