using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response.Items;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogRarityService : BaseDataService<ApplicationDbContext>, ICatalogRarityService
    {
        private readonly ICatalogRarityRepository _catalogRarityRepository;
        private readonly IMapper _mapper;

        public CatalogRarityService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogRarityRepository catalogRarityRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogRarityRepository = catalogRarityRepository;
            _mapper = mapper;
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

        public async Task<int?> AddAsync(string rarity)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogRarityRepository.AddAsync(rarity);
            });
        }

        public async Task<bool> UpdateAsync(int id, string rarity)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _catalogRarityRepository.UpdateAsync(id, rarity);
            });
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await ExecuteSafeAsync(async () => await _catalogRarityRepository.DeleteAsync(id));
        }
    }
}
