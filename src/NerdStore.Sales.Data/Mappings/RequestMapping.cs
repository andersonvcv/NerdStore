using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data.Mappings;

public class RequestMapping : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Code).HasDefaultValueSql("NEXT VALUE FOR MySequence");

        builder.HasMany(r => r.RequestItems).WithOne(ri => ri.Request).HasForeignKey(ri => ri.RequestId);

        builder.ToTable("Requests");
    }
}