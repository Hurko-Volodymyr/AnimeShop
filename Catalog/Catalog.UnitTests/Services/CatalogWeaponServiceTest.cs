using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Services;

namespace Catalog.UnitTests.Services
{
    public class CatalogWeaponServiceTest
    {
        private readonly ICatalogWeaponService _catalogService;
        private readonly Mock<ICatalogWeaponRepository> _catalogWeaponRepository;
        private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<CatalogService>> _logger;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<ILogger<CatalogWeaponService>> _loggerService;

        private readonly CatalogWeapon _testItem = new CatalogWeapon()
        {
            Id = 1,
            Weapon = "Weapon"
        };

        public CatalogWeaponServiceTest()
        {
            _catalogWeaponRepository = new Mock<ICatalogWeaponRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _logger = new Mock<ILogger<CatalogService>>();
            _loggerService = new Mock<ILogger<CatalogWeaponService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogService = new CatalogWeaponService(_dbContextWrapper.Object, _logger.Object, _catalogWeaponRepository.Object, _loggerService.Object, _mapper.Object);
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange
            var testResult = 1;

            _catalogWeaponRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.AddAsync(_testItem.Weapon);

            // assert
            result.Should().Be(testResult);
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange
            int testResult = default;

            _catalogWeaponRepository.Setup(s => s.AddAsync(
                It.IsAny<string>())).ReturnsAsync(testResult);

            // act
            var result = await _catalogService.AddAsync(_testItem.Weapon);

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

            _catalogWeaponRepository.Setup(s => s.UpdateAsync(
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
            _catalogWeaponRepository.Setup(s => s.UpdateAsync(
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
            _catalogWeaponRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(testStatus);

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
            _catalogWeaponRepository.Setup(s => s.DeleteAsync(
                It.IsAny<int>())).ReturnsAsync(It.IsAny<bool>);

            // act
            var result = await _catalogService.DeleteAsync(id);

            // assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetCatalogWeaponsAsync_Success()
        {
            var weapons = new PaginatedItems<CatalogWeapon>()
            {
                Data = new List<CatalogWeapon>()
                {
                    new CatalogWeapon()
                    {
                        Weapon = "weapon"
                    }
                }
            };

            var catalogWeapon = new CatalogWeapon()
            {
                Weapon = "weapon"
            };

            var catalogWeaponDto = new CatalogWeaponDto()
            {
                Weapon = "weapon"
            };

            // arrange
            _catalogWeaponRepository.Setup(s => s.GetAsync()).ReturnsAsync(weapons);

            _mapper.Setup(s => s.Map<CatalogWeaponDto>(
                It.Is<CatalogWeapon>(i => i.Equals(catalogWeapon)))).Returns(catalogWeaponDto);

            // act
            var result = await _catalogService.GetCatalogWeaponsAsync();

            // arrange
            result.Should().NotBeNull();
            result?.Data.Should().NotBeNull();
        }

        [Fact]
        public async Task GetCatalogWeaponsAsync_Failed()
        {
            // arrange
            _catalogWeaponRepository.Setup(s => s.GetAsync()).ReturnsAsync((Func<PaginatedItems<CatalogWeapon>>)null!);

            // act
            var result = await _catalogService.GetCatalogWeaponsAsync();

            // assert
            result.Should().BeNull();
        }
    }
}
