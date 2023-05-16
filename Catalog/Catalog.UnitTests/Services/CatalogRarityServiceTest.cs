using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services
{
    public class CatalogRarityServiceTest
    {
        private readonly ICatalogRarityService _catalogService;
        private readonly Mock<ICatalogRarityRepository> _catalogRarityRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CatalogRarityService>> _loggerService;

        private readonly CatalogRarity _testItem = new CatalogRarity()
        {
            Id = 1,
            Rarity = 4
        };

        public CatalogRarityServiceTest()
        {
            _catalogRarityRepository = new Mock<ICatalogRarityRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _mapper = new Mock<IMapper>();
            _loggerService = new Mock<ILogger<CatalogRarityService>>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CatalogRarityService(_dbContextWrapper.Object, _logger.Object, _catalogRarityRepository.Object, _loggerService.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogRarityRepository.Setup(s => s.AddAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.AddAsync(_testItem.Rarity);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int testResult = default;

            _catalogRarityRepository.Setup(s => s.AddAsync(
                It.IsAny<int>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.AddAsync(_testItem.Rarity);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            // arrange
            var testId = 1;
            var testProperty = 4;
            var testStatus = true;

            _catalogRarityRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<int>(i => i.Equals(testProperty)))).ReturnsAsync(testStatus);

            // act
            var result = await _catalogService.UpdateAsync(testId, testProperty);

            // assert
            result.Should().Be(testStatus);
        }

        [Fact]
        public async Task UpdateAsync_Failed()
        {
            // arrange
            _catalogRarityRepository.Setup(s => s.UpdateAsync(
                It.IsAny<int>(),
                It.IsAny<int>())).ReturnsAsync(It.IsAny<bool>);

            // act
            var result = await _catalogService.UpdateAsync(_testItem.Id, -2);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            // arrange
            var testId = 1;
            var testStatus = true;
            _catalogRarityRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(testStatus);

            // act
            var result = await _catalogService.DeleteAsync(testId);

            // assert
            result.Should().Be(testStatus);
        }

        [Fact]
        public async Task DeleteAsync_Failed()
        {
            // arrange
            int id = default;
            _catalogRarityRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(It.IsAny<bool>);

            // act
            var result = await _catalogService.DeleteAsync(id);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetCatalogRaritiesAsync_Success()
        {
            var rarities = new PaginatedItems<CatalogRarity>()
            {
                Data = new List<CatalogRarity>()
                {
                    new CatalogRarity()
                    {
                        Rarity = 4
                    }
                }
            };

            var catalogRarity = new CatalogRarity()
            {
                Rarity = 4
            };

            var catalogRarityDto = new CatalogRarityDto()
            {
                Rarity = 4
            };

            // arrange
            _catalogRarityRepository.Setup(s => s.GetAsync()).ReturnsAsync(rarities);

            _mapper.Setup(s => s.Map<CatalogRarityDto>(
                It.Is<CatalogRarity>(i => i.Equals(catalogRarity)))).Returns(catalogRarityDto);

            // act
            var result = await _catalogService.GetCatalogRaritiesAsync();

            // arrange
            result.Should().NotBeNull();
            result?.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCatalogRaritiesAsync_Failed()
        {
            // arrange
            _catalogRarityRepository.Setup(s => s.GetAsync()).ReturnsAsync((Func<PaginatedItems<CatalogRarity>>)null!);

            // act
            var result = await _catalogService.GetCatalogRaritiesAsync();

            // assert
            result.Should().BeNull();
        }
    }
}
