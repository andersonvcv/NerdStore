using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Sales.Domain;

namespace NerdStore.Sales.Data.Mappings;

public class RequestMapping : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasKey(ri => ri.Id);
        builder.Property(ri => ri.Code).HasDefaultValueSql("NEXT VALUE FOR MySequence");

        builder.HasMany(ri => ri.RequestItems).WithOne(ri => ri.Request).HasForeignKey(r => r.RequestId);

        builder.ToTable("Requests");
    }
}