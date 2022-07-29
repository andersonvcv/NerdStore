using Microsoft.AspNetCore.Mvc;
using NerdStore.Sales.Application.Queries;

namespace NerdStore.WebApplication.MVC.Extensions;

public class CartViewComponent : ViewComponent
{
    private readonly IRequestQueries _requestQueries;
    protected Guid ClientId = Guid.Parse("8677b3cb-8afc-4aab-9293-7df5dc96f258");

    public CartViewComponent(IRequestQueries requestQueries)
    {
        _requestQueries = requestQueries;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cart = await _requestQueries.GetClientCart(ClientId);
        var numberOfItens = cart?.Items.Count ?? 0;

        return View(numberOfItens);
    }
}