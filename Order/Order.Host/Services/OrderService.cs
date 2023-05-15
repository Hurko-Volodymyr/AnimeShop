using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Models;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _loggerService;
        private readonly IOrderRepository _orderRepository;
        private readonly IInternalHttpClientService _httpClient;
        private readonly OrderConfig _config;

        public OrderService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderRepository orderRepository,
            ILogger<OrderService> loggerService,
            IMapper mapper,
            IInternalHttpClientService httpClient,
            IOptions<OrderConfig> config)
            : base(dbContextWrapper, logger)
        {
            _loggerService = loggerService;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<int?> AddOrderAsync(string userId, string gameAccountId, string name, string lastName, string email)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var basket = await _httpClient.SendAsync<BasketModel, GetBasketRequest>(
                    $"{_config.BasketUrl}/GetFromBasket",
                    HttpMethod.Post,
                    new GetBasketRequest
                    {
                        UserId = userId
                    });

                if (basket == default)
                {
                    _loggerService.LogError($"Basket with customer id = {userId} does not exist");
                    return 0;
                }

                if (basket.BasketItems.Count == 0)
                {
                    _loggerService.LogError($"Basket with customer id = {userId} is empty");
                    return 0;
                }

                var orderId = await _orderRepository.AddOrderAsync(userId, gameAccountId, name, lastName, email, basket.TotalSum, basket.BasketItems);

                if (orderId == 0)
                {
                    _loggerService.LogError("Failed adding order");
                    return 0;
                }

                _loggerService.LogInformation($"Order with id = {orderId} has been added");
                return orderId;
            });
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            if (orders!.Count() == 0)
            {
                _loggerService.LogWarning($"Not founded orders with customer id = {userId}");
                return null!;
            }

            return orders!.Select(s => _mapper.Map<OrderResponse>(s));
        }
    }
}
