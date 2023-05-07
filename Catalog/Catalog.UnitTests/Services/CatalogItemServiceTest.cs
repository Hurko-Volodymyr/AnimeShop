using System.Threading;
using Catalog.Host.Data.Entities;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    private readonly CatalogItem _testItem = new CatalogItem()
    {
        Name = "Name",
        Region = "Region",
        Birthday = "Birthday",
        CatalogWeaponId = 1,
        CatalogRarityId = 1,
        PictureFileURL = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(It.IsAny<CancellationToken>())).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.Add(
               It.IsAny<string>(),
               It.IsAny<string>(),
               It.IsAny<string>(),
               It.IsAny<int>(),
               It.IsAny<int>(),
               It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Region, _testItem.Birthday, _testItem.CatalogWeaponId, _testItem.CatalogRarityId, _testItem.PictureFileURL);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int testResult = default;

        _catalogItemRepository.Setup(s => s.Add(
               It.IsAny<string>(),
               It.IsAny<string>(),
               It.IsAny<string>(),
               It.IsAny<int>(),
               It.IsAny<int>(),
               It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.AddAsync(_testItem.Name, _testItem.Region, _testItem.Birthday, _testItem.CatalogWeaponId, _testItem.CatalogRarityId, _testItem.PictureFileURL);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testId = 1;
        var testStringProperty = "testProperty";
        var testNumberProperty = 1;
        var testStatus = true;
        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.Is<int>(i => i.Equals(testId)),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testStatus);

        // act
        var result = await _catalogService.UpdateAsync(testId, testStringProperty, testStringProperty, testStringProperty, testNumberProperty, testNumberProperty, testStringProperty);

        // assert
        result.Should().Be(testStatus);
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        var testId = 23598723;
        var testStringProperty = "testProperty";
        var testNumberProperty = 1;
        var testStatus = false;
        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.Is<int>(i => i.Equals(testId)),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<string>())).ReturnsAsync(testStatus);

        // act
        var result = await _catalogService.UpdateAsync(testId, testStringProperty, testStringProperty, testStringProperty, testNumberProperty, testNumberProperty, testStringProperty);

        // assert
        result.Should().Be(testStatus);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testId = 1;
        var testStatus = true;
        _catalogItemRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(testStatus);

        // act
        var result = await _catalogService.DeleteAsync(testId);

        // assert
        result.Should().Be(testStatus);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 3331;
        var testStatus = false;
        _catalogItemRepository.Setup(s => s.DeleteAsync(It.Is<int>(i => i == testId))).ReturnsAsync(testStatus);

        // act
        var result = await _catalogService.DeleteAsync(testId);

        // assert
        result.Should().Be(testStatus);
    }
}