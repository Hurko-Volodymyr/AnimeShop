using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Items
{
    public class ItemsByRarityRequest
    {
        [Required]
        [StringLength(1, ErrorMessage = "{0} Rarity must be 1", MinimumLength = 1)]
        public int Rarity { get; set; }
    }
}
