using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NerdStore.Payment.Data.Mappings;

public class PaymentMapping : IEntityTypeConfiguration<Business.Payment>
{
    public void Configure(EntityTypeBuilder<Business.Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.CardName).IsRequired().HasColumnType("varchar(250)");
        builder.Property(p => p.CardNumber).IsRequired().HasColumnType("varchar(16)");
        builder.Property(p => p.CardExiprationDate).IsRequired().HasColumnType("varchar(10)");
        builder.Property(p => p.CardCVV).IsRequired().HasColumnType("varchar(4)");

        builder.HasOne(p => p.Transaction).WithOne(t => t.Payment);

        builder.ToTable("Payments");
    }
}