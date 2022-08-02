using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Payment.Business;

namespace NerdStore.Payment.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Business.Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.ToTable("Transactions");
    }
}