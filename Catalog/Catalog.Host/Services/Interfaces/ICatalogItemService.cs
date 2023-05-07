namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> AddAsync(string name, string region, string birthday, int catalogWeaponId, int catalogRarityId, string pictureFileName);
    Task<bool> UpdateAsync(int id, string name, string region, string birthday, int catalogRarityId, int catalogWeaponId, string pictureFile);
    Task<bool> DeleteAsync(int id);
}