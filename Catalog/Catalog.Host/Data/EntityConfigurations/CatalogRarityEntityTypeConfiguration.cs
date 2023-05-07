using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogRarityEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogRarity>
{
    public void Configure(EntityTypeBuilder<CatalogRarity> builder)
    {
        builder.ToTable("CatalogRarity");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_type_hilo")
            .IsRequired();

        builder.Property(cb => cb.Rarity)
            .IsRequired()
            .HasMaxLength(100);
    }
}