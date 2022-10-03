using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data.Mappings;

public class VoucherMapping : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Code).IsRequired().HasColumnType("varchar(100)");

        builder.HasMany(v => v.Requests).WithOne(r => r.Voucher).HasForeignKey(v => v.VoucherId).IsRequired(false);

        builder.ToTable("Vouchers");
    }
}