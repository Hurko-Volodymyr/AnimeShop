namespace Basket.Host.Models.Requests
{
    public class GetFromBasketRequest
    {
        [Required]
        public string UserId { get; set; } = null!;
    }
}
