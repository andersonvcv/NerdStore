using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data.Mappings;

public class RequestItemMapping : IEntityTypeConfiguration<RequestItem>
{
    public void Configure(EntityTypeBuilder<RequestItem> builder)
    {
        builder.HasKey(ri => ri.Id);
        builder.Property(ri => ri.ProductName).IsRequired().HasColumnType("varchar(250)");

        builder.HasOne(ri => ri.Request).WithMany(r => r.RequestItems);

        builder.ToTable("RequestItems");
    }
}