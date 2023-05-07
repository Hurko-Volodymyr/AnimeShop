using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Response.Items;
using Moq;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<ICatalogRarityRepository> _catalogRarityRepository;
    private readonly Mock<ICatalogWeaponRepository> _catalogWeaponRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogRarityRepository = new Mock<ICatalogRarityRepository>();
        _catalogWeaponRepository = new Mock<ICatalogWeaponRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItem()
        {
            Name = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;
        PaginatedItems<CatalogItem> item = null!;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(item);

        // act
        var result = await _catalogService.GetCatalogItemsAsync(testPageSize, testPageIndex, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Success()
    {
        // arrange
        var testId = 1;
        var characterSuccess = new CatalogItem()
        {
            Name = "TestName"
        };
        var characterDtoSuccess = new CatalogItemDto()
        {
            Name = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByIdAsync(It.Is<int>(i => i == testId))).ReturnsAsync(characterSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(It.Is<CatalogItem>(i => i.Equals(characterSuccess)))).Returns(characterDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testId);

        // assert
        result.Should().NotBeNull();
        result?.Name.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Failed()
    {
        // arrange
        int testId = default;
        _catalogItemRepository.Setup(s => s.GetByIdAsync(It.Is<int>(i => i.Equals(testId)))).Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemByIdAsync(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByWeaponAsync_Success()
    {
        // arrange
        var weapon = "Weapon";

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    CatalogWeapon = new CatalogWeapon()
                    {
                        Weapon = weapon
                    }
                },
            },
        };

        var catalogItemSuccess = new CatalogItem()
        {
            CatalogWeapon = new CatalogWeapon()
            {
                Weapon = weapon
            }
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            CatalogWeapon = new CatalogWeaponDto()
            {
                Weapon = weapon
            }
        };

        _catalogItemRepository.Setup(s => s.GetByWeaponAsync(
            It.IsAny<string>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByWeaponAsync(weapon);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByWeaponAsync_Failed()
    {
        // arrange
        var weapon = "Weapon";

        PaginatedItems<CatalogItem> pagingPaginatedItems = default!;

        var catalogItemSuccess = new CatalogItem()
        {
            CatalogWeapon = new CatalogWeapon()
            {
                Weapon = weapon
            }
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            CatalogWeapon = new CatalogWeaponDto()
            {
                Weapon = weapon
            }
        };

        _catalogItemRepository.Setup(s => s.GetByWeaponAsync(
            It.IsAny<string>())).ReturnsAsync(pagingPaginatedItems);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByWeaponAsync(weapon);

        // assert
        result.Should().BeNull();
        result?.Data.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByRarityAsync_Success()
    {
        // arrange
        var rarity = "Rarity";

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
        {
            Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    CatalogRarity = new CatalogRarity()
                    {
                        Rarity = rarity
                    }
                },
            },
        };

        var catalogItemSuccess = new CatalogItem()
        {
            CatalogRarity = new CatalogRarity()
            {
                Rarity = rarity
            }
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            CatalogRarity = new CatalogRarityDto()
            {
                Rarity = rarity
            }
        };

        _catalogItemRepository.Setup(s => s.GetByRarityAsync(
            It.IsAny<string>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByRarityAsync(rarity);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemsByRarityAsync_Failed()
    {
        // arrange
        var rarity = "Rarity";

        PaginatedItems<CatalogItem> pagingPaginatedItems = default!;

        var catalogItemSuccess = new CatalogItem()
        {
            CatalogRarity = new CatalogRarity()
            {
                Rarity = rarity
            }
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            CatalogRarity = new CatalogRarityDto()
            {
                Rarity = rarity
            }
        };

        _catalogItemRepository.Setup(s => s.GetByRarityAsync(
            It.IsAny<string>())).ReturnsAsync(pagingPaginatedItems);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemsByRarityAsync(rarity);

        // assert
        result.Should().BeNull();
        result?.Data.Should().BeNull();
    }
}