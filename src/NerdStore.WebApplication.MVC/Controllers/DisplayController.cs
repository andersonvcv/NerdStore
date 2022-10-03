using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;

namespace NerdStore.WebApplication.MVC.Controllers
{
    public class DisplayController : Controller
    {
        private readonly IProductApplicationService _productApplicationService;

        public DisplayController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        [HttpGet]
        [Route("")]
        [Route("display")]
        public async Task<IActionResult> Index()
        {
            return View(await _productApplicationService.GetProducts());
        }

        [HttpGet]
        [Route("product-detail/{id}")]
        public async Task<IActionResult> ProductDetail(Guid id)
        {
            return View(await _productApplicationService.GetById(id));
        }
    }
}
