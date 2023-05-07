namespace Basket.Host.Models.Requests
{
    public class DeleteItemRequest
    {
        [Required]
        public string UserId { get; set; } = null!;

        public int ItemId { get; set; }
    }
}
