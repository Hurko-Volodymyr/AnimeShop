using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogWeaponService : BaseDataService<ApplicationDbContext>, ICatalogWeaponService
    {
        private readonly ICatalogWeaponRepository _catalogWeaponRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogWeaponService> _loggerService;

        public CatalogWeaponService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogWeaponRepository catalogRarityRepository,
            ILogger<CatalogWeaponService> loggerService,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogWeaponRepository = catalogRarityRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<PaginatedItemsResponse<CatalogWeaponDto>> GetCatalogWeaponsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogWeaponRepository.GetAsync();

                if (result.Data.Count() == 0)
                {
                    throw new Exception($"Weapon not found");
                }

                return new PaginatedItemsResponse<CatalogWeaponDto>()
                {
                    Data = result.Data.Select(s => _mapper.Map<CatalogWeaponDto>(s)).ToList()
                };
            });
        }

        public async Task<int?> AddAsync(string weapon)
        {
            return await ExecuteSafeAsync(async () =>
            {
                _loggerService.LogInformation($"Weapon {weapon} was added");
                return await _catalogWeaponRepository.AddAsync(weapon);
            });
        }

        public async Task<bool> UpdateAsync(int id, string weapon)
        {
            return await ExecuteSafeAsync(async () =>
            {
                _loggerService.LogInformation($"Weapon with Id {id} was updated");
                return await _catalogWeaponRepository.UpdateAsync(id, weapon);
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _loggerService.LogInformation($"Weapon with Id {id} was deleted");
            return await ExecuteSafeAsync(async () => await _catalogWeaponRepository.DeleteAsync(id));
        }
    }
}