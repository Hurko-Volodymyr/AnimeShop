using Catalog.Host.Models.Requests.Items;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogitem")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateCharacterRequest request)
    {
        var result = await _catalogItemService.AddAsync(request.Name, request.Region, request.Birthday, request.CatalogRarityId, request.CatalogWeaponId, request.PictureUrl);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateCharacterRequest request)
    {
        var result = await _catalogItemService.UpdateAsync(id, request.Name, request.Region, request.Birthday, request.CatalogRarityId, request.CatalogWeaponId, request.PictureUrl);
        return Ok(new UpdateItemResponse<bool>() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogItemService.DeleteAsync(id);
        return Ok(new DeleteItemResponse<bool>() { IsDeleted = result });
    }
}