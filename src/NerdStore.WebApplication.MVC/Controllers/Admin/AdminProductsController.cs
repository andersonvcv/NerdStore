using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.Services;

namespace NerdStore.WebApplication.MVC.Controllers.Admin
{
    public class AdminProductsController : Controller
    {

        private readonly IProductApplicationService _productApplicationService;

        public AdminProductsController(IProductApplicationService productApplicationService)
        {
            _productApplicationService = productApplicationService;
        }

        [HttpGet]
        [Route("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productApplicationService.GetProducts());
        }

        [Route("new-product")]
        public async Task<IActionResult> NewProduct()
        {
            return View(await PopulateCategory(new ProductDTO()));
        }

        [HttpPost]
        [Route("new-products")]
        public async Task<IActionResult> NewProduct(ProductDTO productDto)
        {
            if (ModelState.IsValid) return View(await PopulateCategory(productDto));
            await _productApplicationService.Add(productDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id)
        {
            return View(await _productApplicationService.GetById(id));
        }

        [HttpPost]
        [Route("update-product")]
        public async Task<IActionResult> UpdateProduct(Guid id, ProductDTO productDTO)
        {
            var product = await _productApplicationService.GetById(id);
            productDTO.QuantityInStock = product.QuantityInStock;

            ModelState.Remove("QuantityInStock");
            if(!ModelState.IsValid) return View(await PopulateCategory(productDTO));

            await _productApplicationService.Update(productDTO);

            return RedirectToAction("Index");

            return View(await _productApplicationService.GetById(id));
        }

        [HttpGet]
        [Route("update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id)
        {
            return View("Stock", await _productApplicationService.GetById(id));
        }

        [HttpPost]
        [Route("update-stock")]
        public async Task<IActionResult> UpdateStock(Guid id, int quantity)
        {
            if (quantity > 0)
            {
                await _productApplicationService.AddToStock(id, quantity);
            }
            else
            {
                await _productApplicationService.RemoveFromStock(id, quantity);
            }

            return View("Index", await _productApplicationService.GetProducts());
        }

        private async Task<ProductDTO> PopulateCategory(ProductDTO productDto)
        {
            productDto.Categories = await _productApplicationService.GetCategories();
            return productDto;
        }
    }
}
