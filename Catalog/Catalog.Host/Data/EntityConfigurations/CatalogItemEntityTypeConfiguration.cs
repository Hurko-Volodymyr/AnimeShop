using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogItem>
{
    public void Configure(EntityTypeBuilder<CatalogItem> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_hilo")
            .IsRequired();

        builder.Property(ci => ci.Name)
            .IsRequired(true)
            .HasMaxLength(50);

        builder.Property(ci => ci.Region)
               .IsRequired(true)
               .HasMaxLength(50);

        builder.Property(ci => ci.Birthday)
            .IsRequired(true);

        builder.Property(ci => ci.PictureFileURL)
            .IsRequired(false);

        builder.HasOne(ci => ci.CatalogWeapon)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogWeaponId);

        builder.HasOne(ci => ci.CatalogRarity)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogRarityId);
    }
}