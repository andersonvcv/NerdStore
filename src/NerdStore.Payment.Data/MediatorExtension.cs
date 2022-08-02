using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Payment.Data;

public static class MediatorExtension
{
    public static async Task PublishEvents(this IMediatoRHandler mediatoRHandler, PaymentContext paymentContext)
    {
        var domainEntities = paymentContext.ChangeTracker.Entries<Entity>()
            .Where(e => e.Entity.Events is not null && e.Entity.Events.Any()).ToList();

        var domainEvents = domainEntities.SelectMany(d => d.Entity.Events).ToList();

        domainEntities.ForEach(e => e.Entity.ClearEvents());

        var tasks = domainEvents.Select(async e => await mediatoRHandler.PublishEvent(e));

        await Task.WhenAll(tasks);
    }
}