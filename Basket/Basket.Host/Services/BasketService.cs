using Basket.Host.Models;
using Basket.Host.Services.Abstractions;

namespace Basket.Host.Services
{
    public class BasketService : IBasketService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<BasketService> _logger;

        public BasketService(ICacheService cacheService, ILogger<BasketService> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<bool> AddToBasketAsync(string userId, ItemToBasketModel item)
        {
            var result = await _cacheService.GetAsync<BasketModel>(userId);

            if (result == null)
            {
                _logger.LogError($"Not founded basket for customer id = {userId}");
                result = new BasketModel();
            }

            var basketItem = result.BasketItems.FirstOrDefault(f => f.Id == item.Id);

            if (basketItem != null)
            {
                basketItem.Count++;
                result.TotalSum += item.Price;
                await _cacheService.AddOrUpdateAsync(userId, result);
                _logger.LogInformation($"Count for item with id = {item.Id} was updated");
            }
            else
            {
                result.BasketItems.Add(new BasketItemModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Region = item.Region,
                    Price = item.Price,
                    PictureUrl = item.PictureUrl,
                    Count = 1
                });

                result.TotalSum += item.Price;
                await _cacheService.AddOrUpdateAsync(userId, result);
                _logger.LogInformation($"Item with id = {item.Id} was added to basket");
            }

            return true;
        }

        public async Task<BasketModel> GetBasketAsync(string userId)
        {
            var result = await _cacheService.GetAsync<BasketModel>(userId);

            if (result == null)
            {
                _logger.LogError($"Not founded basket for customer with id = {userId}");
                result = new BasketModel();
            }

            return result;
        }

        public async Task<bool> DeleteBasketItemAsync(string userId, int basketItemId)
        {
            var result = await _cacheService.GetAsync<BasketModel>(userId);

            if (result == null)
            {
                _logger.LogError($"Not founded item for delete with customer id = {userId}");
                return false;
            }

            var basketItem = result.BasketItems.FirstOrDefault(f => f.Id == basketItemId);

            if (basketItem == null)
            {
                _logger.LogError($"Basket item with id = {basketItemId} not found");
                return false;
            }

            if (basketItem.Count > 1)
            {
                basketItem.Count--;
                result.TotalSum -= basketItem.Price;
                await _cacheService.AddOrUpdateAsync(userId, result);
                _logger.LogInformation($"Item's count with id = {basketItem.Id} was updated");
                return true;
            }

            result.BasketItems.Remove(basketItem);
            result.TotalSum -= basketItem.Price * basketItem.Count;

            await _cacheService.AddOrUpdateAsync(userId, result);
            _logger.LogInformation($"Basket item with id = {basketItemId} was deleted");

            return true;
        }

        public async Task<bool> ClearBasketAsync(string userId)
        {
            var result = await _cacheService.RemoveAsync(userId);
            _logger.LogInformation($"Basket was deleted");
            return result;
        }
    }
}
