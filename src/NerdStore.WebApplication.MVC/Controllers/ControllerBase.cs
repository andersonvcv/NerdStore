using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApplication.MVC.Controllers;

public class ControllerBase : Controller
{
    protected Guid ClientId = Guid.Parse("8677b3cb-8afc-4aab-9293-7df5dc96f258");
}