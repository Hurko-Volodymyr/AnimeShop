using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Rarities
{
    public class CreateUpdateRarityRequest
    {
        [Required]
        [StringLength(5, ErrorMessage = "{0} Rarity must be less then 5", MinimumLength = 1)]
        public string Rarity { get; set; } = null!;
    }
}
