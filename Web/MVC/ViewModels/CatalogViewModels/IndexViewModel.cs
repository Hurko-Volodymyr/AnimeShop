using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels;

public class IndexViewModel
{
    public IEnumerable<CatalogItem> CatalogItems { get; set; }
    public IEnumerable<SelectListItem> Rarities { get; set; }
    public IEnumerable<SelectListItem> Weapons { get; set; }
    public int? CharactersFilterApplied { get; set; }
    public int? WeaponsFilterApplied { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}
