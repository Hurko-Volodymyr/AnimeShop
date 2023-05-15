using Order.Host.Models;

namespace Order.Host.Repositories.Abstractions
{
    public interface IOrderItemRepository
    {
        Task<int?> AddOrderAsync(string userId, string gameAccountId, string name, string lastName, string email, decimal totalSum, List<BasketItemModel> items);

        Task<OrderEntity?> GetByIdAsync(int id);

        Task<bool> DeleteOrderAsync(OrderEntity order);
    }
}
