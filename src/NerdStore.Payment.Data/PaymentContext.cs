using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Payment.Business;

namespace NerdStore.Payment.Data;

public class PaymentContext : DbContext, IUnitOfWork
{
    private readonly IMediatoRHandler _mediatoRHandler;

    public PaymentContext(DbContextOptions<PaymentContext> options, IMediatoRHandler mediatoRHandler) : base(options)
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            property.SetColumnType("varchar(100)");

        modelBuilder.Ignore<Event>();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        base.OnModelCreating(modelBuilder);
    }
}