using Catalog.Host.Models.Requests.Weapons;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogWeaponController : ControllerBase
{
    private readonly ICatalogWeaponService _catalogWeaponService;
    private readonly ILogger<CatalogWeaponController> _logger;

    public CatalogWeaponController(
        ICatalogWeaponService catalogWeaponService,
        ILogger<CatalogWeaponController> logger)
    {
        _catalogWeaponService = catalogWeaponService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateUpdateWeaponRequest request)
    {
        var result = await _catalogWeaponService.AddAsync(request.Weapon);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(UpdateItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, CreateUpdateWeaponRequest request)
    {
        var result = await _catalogWeaponService.UpdateAsync(id, request.Weapon);
        return Ok(new UpdateItemResponse<bool>() { IsUpdated = result });
    }

    [HttpPost("{id}")]
    [ProducesResponseType(typeof(DeleteItemResponse<bool>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogWeaponService.DeleteAsync(id);
        return Ok(new DeleteItemResponse<bool>() { IsDeleted = result });
    }
}