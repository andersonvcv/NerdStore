using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data;
using NerdStore.Core.Messages;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        public DbSet<Request> Request { get; set; }
        public DbSet<RequestItem> RequestItem { get; set; }
        public DbSet<Voucher> Voucher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            modelBuilder.HasSequence<int>("MySequence").StartsAt(1000).IncrementsBy(1);

            base.OnModelCreating(modelBuilder);
        }

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

            return await base.SaveChangesAsync() > 0;
        }
    }
}