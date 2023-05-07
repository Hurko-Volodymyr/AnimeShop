using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services
{
    public class CatalogRarityServiceTest
    {
        private readonly ICatalogRarityService _catalogService;
        private readonly Mock<ICatalogRarityRepository> _catalogRarityRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IMapper> _mapper;

        private readonly CatalogRarity _testItem = new CatalogRarity()
        {
            Id = 1,
            Rarity = "Rarity"
        };

        public CatalogRarityServiceTest()
        {
            _catalogRarityRepository = new Mock<ICatalogRarityRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CatalogRarityService(_dbContextWrapper.Object, _logger.Object, _catalogRarityRepository.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogRarityRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

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
                It.IsAny<string>())).ReturnsAsync(testResult);

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
            var testProperty = "testProperty";
            var testStatus = true;

            _catalogRarityRepository.Setup(s => s.UpdateAsync(
                It.Is<int>(i => i.Equals(testId)),
                It.Is<string>(i => i.Equals(testProperty)))).ReturnsAsync(testStatus);

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
                It.IsAny<string>())).ReturnsAsync(It.IsAny<bool>);

            // act
            var result = await _catalogService.UpdateAsync(_testItem.Id, string.Empty);

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
    }
}
