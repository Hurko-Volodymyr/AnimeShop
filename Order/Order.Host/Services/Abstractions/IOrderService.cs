namespace Order.Host.Services.Abstractions
{
    public interface IOrderService
    {
        Task<int?> AddOrderAsync(string userId, string gameAccountId, string name, string lastName, string email);
        Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(string userId);
    }
}
