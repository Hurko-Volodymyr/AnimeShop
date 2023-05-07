using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Items;

public class CreateCharacterRequest
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string Region { get; set; } = null!;

    public string Birthday { get; set; } = null!;

    public string PictureUrl { get; set; } = null!;

    public int CatalogWeaponId { get; set; }

    public int CatalogRarityId { get; set; }
}