using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogWeaponService
    {
        Task<PaginatedItemsResponse<CatalogWeaponDto>> GetCatalogWeaponsAsync();

        Task<int?> AddAsync(string weapon);

        Task<bool> UpdateAsync(int id, string weapon);

        Task<bool> DeleteAsync(int id);
    }
}
