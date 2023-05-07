using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Abstractions;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogWeaponId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogRarityId == typeFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
           .Include(i => i.CatalogWeapon)
           .Include(i => i.CatalogRarity)
           .Skip(pageSize * pageIndex)
           .Take(pageSize)
           .ToListAsync();

        return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> AddAsync(string name, string region, string birthday, int catalogWeaponId, int catalogRarityId, string pictureFile)
    {
        var item1 = new CatalogItem
        {
            CatalogWeaponId = catalogWeaponId,
            CatalogRarityId = catalogRarityId,
            Region = region,
            Name = name,
            PictureFile = pictureFile,
            Birthday = birthday
        };
        var item = await _dbContext.AddAsync(item1);

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByRarityAsync(int rarity)
    {
        var result = await _dbContext.CatalogItems
                  .Include(i => i.CatalogRarity).Where(w => w.CatalogRarity!.Rarity == rarity)
                  .ToListAsync();

        return new PaginatedItems<CatalogItem>() { Data = result };
    }

    public async Task<PaginatedItems<CatalogItem>> GetByWeaponAsync(string weapon)
    {
        var result = await _dbContext.CatalogItems
                    .Include(i => i.CatalogWeapon).Where(w => w.CatalogWeapon!.Weapon == weapon)
                    .ToListAsync();

        return new PaginatedItems<CatalogItem>() { Data = result };
    }

    public async Task<CatalogItem?> GetByIdAsync(int id)
    {
        return await _dbContext.CatalogItems.Include(i => i.CatalogWeapon).Include(i => i.CatalogRarity).FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<bool> UpdateAsync(int id, string name, string region, string birthday, int catalogRarityId, int catalogWeaponId, string pictureFile)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(f => f.Id == id);

        if (item == null)
        {
            return false;
        }

        item!.Name = name;
        item!.Region = region;
        item!.Birthday = birthday;
        item!.CatalogRarityId = catalogRarityId;
        item!.CatalogWeaponId = catalogWeaponId;
        item!.PictureFile = pictureFile;

        _dbContext.Entry(item).CurrentValues.SetValues(item);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _dbContext.CatalogItems.FirstOrDefaultAsync(f => f.Id == id);
        if (item == null)
        {
            return false;
        }

        _dbContext.Entry(item).State = EntityState.Deleted;
        await _dbContext.SaveChangesAsync();

        return true;
    }
}