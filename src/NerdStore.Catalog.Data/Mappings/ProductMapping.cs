﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalog.Domain;

namespace NerdStore.Catalog.Data.Mappings
{
    internal class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasColumnType("varchar(250)");
            builder.Property(p => p.Description).IsRequired().HasColumnType("varchar(500)");
            builder.Property(p => p.Image).IsRequired().HasColumnType("varchar(250)");

            builder.OwnsOne(p => p.Dimension, ownedBuilder =>
            {
                ownedBuilder.Property(d => d.Height).HasColumnName("Height").HasColumnType("int");
                ownedBuilder.Property(d => d.Width).HasColumnName("Width").HasColumnType("int");
                ownedBuilder.Property(d => d.Depth).HasColumnName("Depth").HasColumnType("int");
            });

            builder.ToTable("Products");
        }
    }
}
