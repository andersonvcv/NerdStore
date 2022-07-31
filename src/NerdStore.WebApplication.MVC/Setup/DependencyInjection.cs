using MediatR;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data;
using NerdStore.Catalog.Domain;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.IntegrationEvents;
using NerdStore.Core.Messages.Notifications;
using NerdStore.Sales.Application.Commands;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Application.Queries;
using NerdStore.Sales.Data;
using NerdStore.Sales.Domain;
using RequestEventHandler = NerdStore.Catalog.Domain.Events.RequestEventHandler;

namespace NerdStore.WebApplication.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // MediatoR
            services.AddScoped<IMediatoRHandler, MediatoRHandler>();

            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductApplicationService, ProductApplicationService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<CatalogContext>();
            
            services.AddScoped<INotificationHandler<LowStockEvent>, RequestEventHandler>();

            // Sales
            services.AddScoped<IRequestRepository, RequestRepository>();
            services.AddScoped<IRequestQueries, RequestQueries>();
            services.AddScoped<SalesContext>();

            services.AddScoped<IRequestHandler<AddRequestItemCommand, bool>, RequestCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyRequestVoucherCommand, bool>, RequestCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveRequestItemCommand, bool>, RequestCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateRequestItemCommand, bool>, RequestCommandHandler>();
            services.AddScoped<IRequestHandler<InitiateRequestCommand, bool>, RequestCommandHandler>();

            services.AddScoped<INotificationHandler<DraftRequestEvent>, Sales.Application.Events.RequestEventHandler>();
            services.AddScoped<INotificationHandler<UpdatedRequestEvent>, Sales.Application.Events.RequestEventHandler>();
            services.AddScoped<INotificationHandler<AddedRequestItemEvent>, Sales.Application.Events.RequestEventHandler>();
            services.AddScoped<INotificationHandler<InitiatedRequestEvent>, Sales.Application.Events.RequestEventHandler>();

            // Payment
        }
    }
}
