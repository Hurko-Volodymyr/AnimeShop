using Basket.Host.Models;
namespace Basket.Host.Services.Abstractions
{
    public interface IBasketService
    {
        Task<bool> AddToBasketAsync(string userId, ItemToBasketModel item);

        Task<BasketModel> GetBasketAsync(string userId);

        Task<bool> DeleteBasketItemAsync(string userId, int basketItemId);

        Task<bool> ClearBasketAsync(string userId);
    }
}
