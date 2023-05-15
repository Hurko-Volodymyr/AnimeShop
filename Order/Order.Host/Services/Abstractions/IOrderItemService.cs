using Order.Host.Models;
using Order.Host.Models.Dtos;

namespace Order.Host.Services.Abstractions
{
    public interface IOrderItemService
    {
        Task<int?> AddAsync(string userId, string gameAccountId, string name, string lastName, string email, BasketItemModel[] basketItems, decimal totalSum);

        Task<OrderDto?> GetOrderByIdAsync(int id);

        Task<bool> DeleteOrderAsync(int id);
    }
}
