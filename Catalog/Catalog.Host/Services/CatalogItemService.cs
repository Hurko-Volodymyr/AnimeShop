using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Abstractions;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ILogger<CatalogItemService> _loggerService;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        ILogger<CatalogItemService> loggerService)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _loggerService = loggerService;
    }

    public async Task<int?> AddAsync(string name, string region, string birthday, int catalogWeaponId, int catalogRarityId, string pictureFileName)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var id = await _catalogItemRepository.AddAsync(name, region, birthday, catalogWeaponId, catalogRarityId, pictureFileName);
            _loggerService.LogInformation($"Add character with Id = {id}");
            return id;
        });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var isItemDelete = await _catalogItemRepository.DeleteAsync(id);
            if (isItemDelete == default)
            {
                _loggerService.LogInformation($"Character with Id = {id} not found");
            }
            else
            {
                _loggerService.LogInformation($"Delete character with Id = {id}");
            }

            return isItemDelete;
        });
    }

    public async Task<bool> UpdateAsync(int id, string name, string region, string birthday, int catalogRarityId, int catalogWeaponId, string pictureFile)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var isItemUpdate = await _catalogItemRepository.UpdateAsync(id, name, region, birthday, catalogRarityId, catalogWeaponId, pictureFile);
            if (isItemUpdate == default)
            {
                _loggerService.LogInformation($"Character with Id = {id} not found");
            }
            else
            {
                _loggerService.LogInformation($"Update character with Id = {id}");
            }

            return isItemUpdate;
        });
    }
}