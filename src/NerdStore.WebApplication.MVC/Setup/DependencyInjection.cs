using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.MediatR;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Domain;

namespace NerdStore.WebApplication.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // MediatR
            services.AddScoped<IMediatRHandler, MediatRHandler>();
            
            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();
            
            services.AddScoped<INotificationHandler<LowStockEvent>, LowStockEventHandler>();

            // Sales
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestHandler<AddRequestItemCommand, bool>, RequestCommandHandler>();
        }
    }
}
