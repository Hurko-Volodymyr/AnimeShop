using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogRarityRepository
    {
        Task<PaginatedItems<CatalogRarity>> GetAsync();

        Task<CatalogRarity?> GetByIdAsync(int id);

        Task<int?> AddAsync(string rarity);

        Task<bool> UpdateAsync(int id, string rarity);

        Task<bool> DeleteAsync(int id);
    }
}
