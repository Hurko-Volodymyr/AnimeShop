using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Abstractions;

namespace Catalog.Host.Services
{
    public class CatalogRarityService : BaseDataService<ApplicationDbContext>, ICatalogRarityService
    {
        private readonly ICatalogRarityRepository _catalogRarityRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogRarityService> _loggerService;

        public CatalogRarityService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogRarityRepository catalogRarityRepository,
            ILogger<CatalogRarityService> loggerService,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogRarityRepository = catalogRarityRepository;
            _mapper = mapper;
            _loggerService = loggerService;
        }

        public async Task<PaginatedItemsResponse<CatalogRarityDto>> GetCatalogRaritiesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogRarityRepository.GetAsync();

                if (result.Data.Count() == 0)
                {
                    throw new Exception($"Rarity not found");
                }

                return new PaginatedItemsResponse<CatalogRarityDto>()
                {
                    Data = result.Data.Select(s => _mapper.Map<CatalogRarityDto>(s)).ToList()
                };
            });
        }

        public async Task<int?> AddAsync(int rarity)
        {
            return await ExecuteSafeAsync(async () =>
            {
                _loggerService.LogInformation($"Rarity {rarity} was added");
                return await _catalogRarityRepository.AddAsync(rarity);
            });
        }

        public async Task<bool> UpdateAsync(int id, int rarity)
        {
            return await ExecuteSafeAsync(async () =>
            {
                _loggerService.LogInformation($"Rarity with Id = {id} was updated");
                return await _catalogRarityRepository.UpdateAsync(id, rarity);
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            _loggerService.LogInformation($"Rarity with Id = {id} was deleted");
            return await ExecuteSafeAsync(async () => await _catalogRarityRepository.DeleteAsync(id));
        }
    }
}
