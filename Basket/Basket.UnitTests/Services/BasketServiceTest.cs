namespace Basket.UnitTests.Services
{
    public class BasketServiceTest
    {
        private readonly IBasketService _basketService;

        private readonly Mock<ICacheService> _cacheService;
        private readonly Mock<ILogger<BasketService>> _logger;

        private readonly string _testId = "testId";

        private readonly ItemToBasketModel _testItemToBasketModel = new()
        {
            Id = 1,
            PictureUrl = "test",
            Region = "test",
            Price = 1,
            Name = "Test",
        };

        private readonly BasketItemModel _testBasketItemModel = new()
        {
            Id = 1,
            Name = "Test",
            Region = "test",
            Price = 1,
            PictureUrl = "Test",
            Count = 1
        };

        public BasketServiceTest()
        {
            _cacheService = new Mock<ICacheService>();
            _logger = new Mock<ILogger<BasketService>>();

            _basketService = new BasketService(_cacheService.Object, _logger.Object);
        }

        [Fact]
        public async void GetBasketAsync_Success()
        {
            // arrange
            int testSum = 1;

            var basketModelSuccess = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(
                It.Is<string>(i => i == _testId))).ReturnsAsync(basketModelSuccess);

            // act
            var result = await _basketService.GetBasketAsync(_testId);

            // assert
            result.Should().NotBeNull();
            result?.BasketItems.Should().NotBeNull();
            result?.TotalSum.Should().Be(testSum);
        }

        [Fact]
        public async Task GetBasketAsync_Failed()
        {
            // arrange
            _cacheService.Setup(x => x.GetAsync<BasketModel>(_testId)).ReturnsAsync((Func<BasketModel>)null!);

            // act
            var result = await _basketService.GetBasketAsync(_testId);

            // assert
            result.Should().NotBeNull();
            result.BasketItems.Count.Should().Be(0);
            result.TotalSum.Should().Be(0);

            _logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Not founded basket for customer with id = {_testId}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task AddItemToBasketAsync_Success()
        {
            // arrange
            int testTotalSum = 1;

            var basketModelSuccess = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(
                It.Is<string>(i => i == _testId))).ReturnsAsync(basketModelSuccess);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModelSuccess)));

            // act
            var result = await _basketService.AddToBasketAsync(_testId, _testItemToBasketModel);

            // assert
            result.Should().BeTrue();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Count for item with id = {_testItemToBasketModel.Id} was updated")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task AddItemToBasketAsync_AddItem_AddAnother_Success()
        {
            // arrange
            int testTotalSum = 1;

            var testProductToBasketModel = new ItemToBasketModel()
            {
                Id = 2,
                PictureUrl = "test",
                Region = "Test",
                Price = 1,
                Name = "Test",
            };

            var basketModelSuccess = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(
                It.Is<string>(i => i == _testId))).ReturnsAsync(basketModelSuccess);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModelSuccess)));

            // act
            var result = await _basketService.AddToBasketAsync(_testId, testProductToBasketModel);

            // assert
            result.Should().BeTrue();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Item with id = {testProductToBasketModel.Id} was added to basket")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task AddItemToBasketAsync_AddItemBasket_Failed()
        {
            // arrange
            int testTotalSum = 1;

            var basketModelFailed = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(x => x.GetAsync<BasketModel>(_testId)).ReturnsAsync((Func<BasketModel>)null!);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModelFailed)));

            // act
            var result = await _basketService.AddToBasketAsync(_testId, _testItemToBasketModel);

            // assert
            result.Should().BeTrue();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Not founded basket for customer id = {_testId}")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task DeleteBasketItemAsync_Success()
        {
            // arrange
            int testTotalSum = 1;
            int testBasketItemId = 1;

            var basketModelSuccess = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    new BasketItemModel()
                    {
                        Id = 1,
                        Name = "Test",
                        Price = 1,
                        Region = "Test",
                        PictureUrl = "Test",
                        Count = 2
                    }
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(
                It.Is<string>(i => i == _testId))).ReturnsAsync(basketModelSuccess);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModelSuccess)));

            // act
            var result = await _basketService.DeleteBasketItemAsync(_testId, testBasketItemId);

            // assert
            result.Should().BeTrue();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Item's count with id = {_testItemToBasketModel.Id} was updated")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task DeleteBasketItemAsync_RemoveAll_Success()
        {
            // arrange
            int testTotalSum = 1;
            int testBasketItemId = 1;

            var basketModelSuccess = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(
                It.Is<string>(i => i == _testId))).ReturnsAsync(basketModelSuccess);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModelSuccess)));

            // act
            var result = await _basketService.DeleteBasketItemAsync(_testId, testBasketItemId);

            // assert
            result.Should().BeTrue();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Basket item with id = {testBasketItemId} was deleted")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task DeleteBasketItemAsync_Failed()
        {
            // arrange
            int testTotalSum = 1;
            int testBasketItemId = 2;

            var basketModel = new BasketModel()
            {
                BasketItems = new List<BasketItemModel>()
                {
                    _testBasketItemModel
                },
                TotalSum = testTotalSum,
            };

            _cacheService.Setup(s => s.GetAsync<BasketModel>(_testId)).ReturnsAsync(basketModel);

            _cacheService.Setup(s => s.AddOrUpdateAsync(
                It.Is<string>(i => i == _testId),
                It.Is<BasketModel>(i => i == basketModel)));

            // act
            var result = await _basketService.DeleteBasketItemAsync(_testId, testBasketItemId);

            // assert
            result.Should().BeFalse();

            _logger.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => o.ToString()!
                        .Contains($"Basket item with id = {testBasketItemId} not found")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
                Times.Once);
        }

        [Fact]
        public async Task ClearBasketAsync_Success()
        {
            // arrange
            var testRemoveResponse = true;

            _cacheService.Setup(s => s.RemoveAsync(_testId)).ReturnsAsync(testRemoveResponse);

            // act
            var result = await _basketService.ClearBasketAsync(_testId);

            // assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task ClearBasketAsync_Failed()
        {
            // arrange
            var testRemoveResponse = false;

            _cacheService.Setup(s => s.RemoveAsync(_testId)).ReturnsAsync(testRemoveResponse);

            // act
            var result = await _basketService.ClearBasketAsync(_testId);

            // assert
            result.Should().BeFalse();
        }
    }
}