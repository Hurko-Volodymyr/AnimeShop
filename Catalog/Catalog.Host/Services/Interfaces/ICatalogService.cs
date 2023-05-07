using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response.Items;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogTypeFilter, int>? filters);
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsByWeaponAsync(string weapon);
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsByRarityAsync(string rarity);
    Task<CatalogItemDto?> GetCatalogItemByIdAsync(int id);
}