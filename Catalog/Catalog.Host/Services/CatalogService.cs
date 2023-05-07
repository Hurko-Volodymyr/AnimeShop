using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogService> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CatalogItemDto?> GetCatalogItemByIdAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByIdAsync(id);

            if (result == null)
            {
                _logger.LogWarning($"Character with Id = {id} not found");
                return null;
            }

            return _mapper.Map<CatalogItemDto>(result);
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters)
    {
        return await ExecuteSafeAsync(async () =>
        {
            int? weaponFilter = null;
            int? rarityFilter = null;

            if (filters != null)
            {
                if (filters.TryGetValue(CatalogTypeFilter.Weapon, out var weapon))
                {
                    weaponFilter = weapon;
                }

                if (filters.TryGetValue(CatalogTypeFilter.Rarity, out var rarity))
                {
                    rarityFilter = rarity;
                }
            }

            var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize, weaponFilter, rarityFilter);
            if (result == null)
            {
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Count = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsByRarityAsync(string rarity)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByRarityAsync(rarity);

            if (result == null)
            {
                _logger.LogWarning($"Characters with Rarity = {rarity} not found");
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList()
            };
        });
    }

    public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsByWeaponAsync(string weapon)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogItemRepository.GetByWeaponAsync(weapon);

            if (result == null)
            {
                _logger.LogWarning($"Characters with weapon = {weapon} not found");
                return null;
            }

            return new PaginatedItemsResponse<CatalogItemDto>()
            {
                Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList()
            };
        });
    }
}