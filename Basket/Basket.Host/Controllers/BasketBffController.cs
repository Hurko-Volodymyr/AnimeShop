using Basket.Host.Models;
using Basket.Host.Models.Requests;
using Basket.Host.Services.Abstractions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Basket.Host.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Scope("basket.basketbff")]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketBffController : ControllerBase
    {
        private readonly ILogger<BasketBffController> _logger;
        private readonly IBasketService _basketService;

        public BasketBffController(
            ILogger<BasketBffController> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddToBasket(AddToBasketRequest request)
        {
            var response = await _basketService.AddToBasketAsync(request.Id, request.BasketItem);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFromBasket(GetFromBasketRequest request)
        {
            var response = await _basketService.GetBasketAsync(request.UserId);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItemFromBasket(DeleteItemRequest request)
        {
            var response = await _basketService.DeleteBasketItemAsync(request.UserId, request.ItemId);
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ClearBasket(ClearBasketRequest request)
        {
            var response = await _basketService.ClearBasketAsync(request.UserId);
            return Ok(response);
        }
    }
}