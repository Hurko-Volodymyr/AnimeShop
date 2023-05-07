using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogRarityRepository : ICatalogRarityRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogRarityRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogRarity>> GetAsync()
        {
            var result = await _dbContext.CatalogRarities
                .ToListAsync();

            return new PaginatedItems<CatalogRarity>() { Data = result };
        }

        public async Task<CatalogRarity?> GetByIdAsync(int id)
        {
            return await _dbContext.CatalogRarities.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int?> AddAsync(string rarity)
        {
            var item = await _dbContext.AddAsync(new CatalogRarity
            {
                Rarity = rarity
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, string rarity)
        {
            var item = await GetByIdAsync(id);
            var status = false;

            if (item != null)
            {
                _dbContext.CatalogRarities.Update(item);
                await _dbContext.SaveChangesAsync();
                status = true;
            }

            return status;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rarity = await _dbContext.CatalogRarities.FirstOrDefaultAsync(f => f.Id == id);
            if (rarity == null)
            {
                return false;
            }

            _dbContext.Entry(rarity).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
