using Catalog.Host.Models.Requests.Rarities;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogRarityController : ControllerBase
{
    private readonly ICatalogRarityService _catalogRarityService;
    private readonly ILogger<CatalogRarityController> _logger;

    public CatalogRarityController(
        ICatalogRarityService catalogRarityService,
        ILogger<CatalogRarityController> logger)
    {
        _catalogRarityService = catalogRarityService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateUpdateRarityRequest request)
    {
        var result = await _catalogRarityService.AddAsync(request.Rarity);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateUpdateRarityRequest request)
    {
        var result = await _catalogRarityService.UpdateAsync(id, request.Rarity);
        return Ok(new UpdateItemResponse<bool>() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogRarityService.DeleteAsync(id);
        return Ok(new DeleteItemResponse<bool>() { IsDeleted = result });
    }
}