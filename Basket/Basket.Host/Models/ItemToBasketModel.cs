namespace Basket.Host.Models
{
    public class ItemToBasketModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Region { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
    }
}
