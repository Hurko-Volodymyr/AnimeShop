using Catalog.Host.Configurations;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests.Items;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly ICatalogRarityService _catalogRarityService;
    private readonly ICatalogWeaponService _catalogWeaponService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config,
        ICatalogRarityService catalogRarityService,
        ICatalogWeaponService catalogWeaponService)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
        _catalogRarityService = catalogRarityService;
        _catalogWeaponService = catalogWeaponService;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogTypeFilter> request)
    {
        var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByRarity(ItemsByRarityRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByRarityAsync(request.Rarity);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByWeapon(ItemsByWeaponRequest request)
    {
        var result = await _catalogService.GetCatalogItemsByWeaponAsync(request.Weapon);
        return Ok(result);
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemById(int id)
    {
        var result = await _catalogService.GetCatalogItemByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogRarityDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Rarities()
    {
        var result = await _catalogRarityService.GetCatalogRaritiesAsync();
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogWeaponDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Weapons()
    {
        var result = await _catalogWeaponService.GetCatalogWeaponsAsync();
        return Ok(result);
    }
}