namespace Basket.Host.Models.Requests
{
    public class AddToBasketRequest
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public ItemToBasketModel BasketItem { get; set; } = null!;
    }
}
