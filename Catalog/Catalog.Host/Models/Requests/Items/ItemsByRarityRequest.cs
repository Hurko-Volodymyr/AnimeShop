using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Items
{
    public class ItemsByRarityRequest
    {
        [Required]
        [StringLength(5, ErrorMessage = "{0} Rarity must be less then 5", MinimumLength = 1)]
        public string Rarity { get; set; } = null!;
    }
}
