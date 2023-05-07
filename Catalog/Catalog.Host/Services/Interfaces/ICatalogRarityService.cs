using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogRarityService
    {
        Task<PaginatedItemsResponse<CatalogRarityDto>> GetCatalogRaritiesAsync();

        Task<int?> AddAsync(int rarity);

        Task<bool> UpdateAsync(int id, int rarity);

        Task<bool> DeleteAsync(int id);
    }
}
