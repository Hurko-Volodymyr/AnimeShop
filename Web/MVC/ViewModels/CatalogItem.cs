namespace MVC.ViewModels;

public record CatalogItem
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Birthday { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public CatalogWeapon CatalogWeapon { get; set; } = null!;

    public CatalogRarity CatalogRarity { get; set; } = null!;
}