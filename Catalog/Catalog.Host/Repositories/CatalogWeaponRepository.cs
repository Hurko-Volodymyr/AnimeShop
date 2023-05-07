using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CatalogWeaponRepository : ICatalogWeaponRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<CatalogItemRepository> _logger;

        public CatalogWeaponRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<CatalogItemRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedItems<CatalogWeapon>> GetAsync()
        {
            var result = await _dbContext.CatalogWeapons
                .ToListAsync();

            return new PaginatedItems<CatalogWeapon>() { Data = result };
        }

        public async Task<CatalogWeapon?> GetByIdAsync(int id)
        {
            return await _dbContext.CatalogWeapons.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<int?> AddAsync(string weapon)
        {
            var item = await _dbContext.AddAsync(new CatalogWeapon
            {
                Weapon = weapon
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, string weapon)
        {
            var item = await GetByIdAsync(id);
            var status = false;

            if (item != null)
            {
                _dbContext.CatalogWeapons.Update(item);
                await _dbContext.SaveChangesAsync();
                status = true;
            }

            return status;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var weapon = await _dbContext.CatalogWeapons.FirstOrDefaultAsync(f => f.Id == id);
            if (weapon == null)
            {
                return false;
            }

            _dbContext.Entry(weapon).State = EntityState.Deleted;
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
