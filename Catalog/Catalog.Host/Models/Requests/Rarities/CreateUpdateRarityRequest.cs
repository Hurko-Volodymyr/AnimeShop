using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Rarities
{
    public class CreateUpdateRarityRequest
    {
        [Required]
        [StringLength(1, ErrorMessage = "{0} Rarity must be 1 symbol", MinimumLength = 1)]
        public int Rarity { get; set; }
    }
}
