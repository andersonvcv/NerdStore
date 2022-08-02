using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Payment.Business;

namespace NerdStore.Payment.Data;

public class PaymentContext : DbContext, IUnitOfWork
{
    private readonly IMediatoRHandler _mediatoRHandler;

    public PaymentContext(DbContextOptions options, IMediatoRHandler mediatoRHandler) : base(options)
    {
        _mediatoRHandler = mediatoRHandler;
    }

    public DbSet<Business.Payment> Payment { get; private set; }
    public DbSet<Transaction> Transaction { get; private set; }


    public async Task<bool> Commit()
    {
        const string entryDateColumnName = "EntryDate";
        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty(entryDateColumnName) != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(entryDateColumnName).CurrentValue = DateTime.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(entryDateColumnName).IsModified = false;
            }
        }

        var persistedData = await base.SaveChangesAsync() > 0;
        if (persistedData) await _mediatoRHandler.PublishEvents(this);
        return persistedData;
    }
}