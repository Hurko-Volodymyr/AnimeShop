namespace Basket.Host.Models.Requests
{
    public class ClearBasketRequest
    {
        [Required]
        public string UserId { get; set; } = null!;
    }
}
