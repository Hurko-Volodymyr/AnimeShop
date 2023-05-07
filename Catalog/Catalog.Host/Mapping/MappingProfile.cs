using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatalogItem, CatalogItemDto>()
            .ForMember("PictureUrl", opt
                => opt.MapFrom<CatalogItemPictureResolver, string>(c => c.PictureFileURL))
             .ForMember(dest => dest.CatalogWeapon, opt
        => opt.MapFrom(src => src.CatalogWeapon));
        CreateMap<CatalogWeapon, CatalogWeaponDto>();
        CreateMap<CatalogRarity, CatalogRarityDto>();
    }
}