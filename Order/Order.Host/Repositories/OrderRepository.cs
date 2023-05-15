using Order.Host.Models;

namespace Order.Host.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper)
        {
            _dbContext = dbContextWrapper.DbContext;
        }

        public async Task<int?> AddOrderAsync(string userId, string gameAccountId, string name, string lastName, string email, decimal totalSum, List<BasketItemModel> items)
        {
            var result = await _dbContext.AddAsync(new OrderEntity
            {
                UserId = userId,
                GameAccountId = gameAccountId,
                Name = name,
                LastName = lastName,
                Email = email,
                CreatedAt = DateTime.UtcNow.Date,
                TotalSum = totalSum
            });

            await _dbContext.OrderDetails.AddRangeAsync(items.Select(s => new OrderDetailsEntity()
            {
                OrderId = result.Entity.Id,
                CatalogItemId = s.Id,
                Count = s.Count
            }));

            await _dbContext.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<IEnumerable<OrderEntity>?> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Orders.Include(i => i.OrderDetails).Where(f => f.UserId == userId).ToListAsync();
        }
    }
}
