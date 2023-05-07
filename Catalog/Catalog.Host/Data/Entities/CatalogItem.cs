namespace Catalog.Host.Data.Entities;

public class CatalogItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Birthday { get; set; } = string.Empty!;

    public string PictureFileURL { get; set; } = null!;

    public int CatalogRarityId { get; set; }

    public CatalogRarity CatalogRarity { get; set; } = null!;

    public int CatalogWeaponId { get; set; }

    public CatalogWeapon CatalogWeapon { get; set; } = null!;
}
