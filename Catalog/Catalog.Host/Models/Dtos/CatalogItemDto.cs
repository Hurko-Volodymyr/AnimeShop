namespace Catalog.Host.Models.Dtos;

public class CatalogItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Birthday { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public CatalogWeaponDto CatalogWeapon { get; set; } = null!;

    public CatalogRarityDto CatalogRarity { get; set; } = null!;
}
