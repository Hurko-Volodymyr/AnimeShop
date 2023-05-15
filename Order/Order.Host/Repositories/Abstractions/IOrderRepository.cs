using Order.Host.Models;

namespace Order.Host.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderEntity>?> GetOrdersByUserIdAsync(string userId);

        Task<int?> AddOrderAsync(string userId, string gameAccountId, string name, string lastName, string email, decimal totalSum, List<BasketItemModel> items);
    }
}
