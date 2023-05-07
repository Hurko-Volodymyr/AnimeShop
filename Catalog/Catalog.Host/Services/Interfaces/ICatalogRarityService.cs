using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogRarityService
    {
        Task<PaginatedItemsResponse<CatalogRarityDto>> GetCatalogRaritiesAsync();

        Task<int?> AddAsync(string rarity);

        Task<bool> UpdateAsync(int id, string rarity);

        Task<bool> DeleteAsync(int id);
    }
}
