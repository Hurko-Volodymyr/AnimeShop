using Catalog.Host.Data.Entities;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogWeaponEntityTypeConfiguration
    : IEntityTypeConfiguration<CatalogWeapon>
{
    public void Configure(EntityTypeBuilder<CatalogWeapon> builder)
    {
        builder.ToTable("CatalogWeapon");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_brand_hilo")
            .IsRequired();

        builder.Property(cb => cb.Weapon)
            .IsRequired()
            .HasMaxLength(100);
    }
}