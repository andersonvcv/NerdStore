﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Application.Queries.DTOs;

namespace NerdStore.WebApplication.MVC.Controllers
{
    public class CartController : ControllerBase
    {
        private readonly IProductApplicationService _productApplicationService;
        private readonly IRequestQueries _requestQueries;
        private readonly IMediatoRHandler _mediatoRHandler;

        public CartController(INotificationHandler<DomainNotification> domainNotificationHandler, IProductApplicationService productApplicationService, IRequestQueries requestQueries, IMediatoRHandler mediatoRHandler) : base(domainNotificationHandler, mediatoRHandler)
        {
            _productApplicationService = productApplicationService;
            _requestQueries = requestQueries;
            _mediatoRHandler = mediatoRHandler;
        }

        [HttpGet]
        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _requestQueries.GetClientCart(ClientId));
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

            TempData["Errors"] = GetErrorMessages();
            return RedirectToAction("ProductDetail", "Display", new { id });
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productApplicationService.GetById(id);
            if (product is null) return BadRequest();

            var command = new RemoveRequestItemCommand(ClientId, id);
            await _mediatoRHandler.SendCommand(command);

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _requestQueries.GetClientCart(ClientId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productApplicationService.GetById(id);
            if (product is null) return BadRequest();

            var command = new UpdateRequestItemCommand(ClientId, id, quantity);
            await _mediatoRHandler.SendCommand(command);

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _requestQueries.GetClientCart(ClientId));
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyRequestVoucherCommand(ClientId, voucherCode);
            await _mediatoRHandler.SendCommand(command);

            if (ValidOperation())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _requestQueries.GetClientCart(ClientId));
        }

        [HttpGet]
        [Route("puchase-summary")]
        public async Task<IActionResult> PurchaseSummary()
        {
            return View(await _requestQueries.GetClientCart(ClientId));
        } 

        [HttpPost]
        [Route("initiate-request")]
        public async Task<IActionResult> InitiateRequest(CartDTO cartDTO)
        {
            var cart = await _requestQueries.GetClientCart(ClientId);

            var command = new InitiateRequestCommand(cart.RequestId, ClientId, cart.Total, cartDTO.Payment.CreditCardName, cartDTO.Payment.CreditCardNumber, cartDTO.Payment.CreditCardExpirationDate, cartDTO.Payment.CreditCardCVV);

            await _mediatoRHandler.SendCommand(command);

            if (ValidOperation())
            {
                return RedirectToAction("Index", "Request");
            }

            return View("PurchaseSummary", await _requestQueries.GetClientCart(ClientId));
        }
    }
}
