using Catalog.Host.Data.Entities;
using Catalog.Host.Data;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogWeaponRepository
    {
        Task<PaginatedItems<CatalogWeapon>> GetAsync();

        Task<CatalogWeapon?> GetByIdAsync(int id);

        Task<int?> AddAsync(string weapon);

        Task<bool> UpdateAsync(int id, string weapon);

        Task<bool> DeleteAsync(int id);
    }
}
