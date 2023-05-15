namespace Order.Host.Models.Responses
{
    public class OrderResponse
    {
        public int Id { get; set; }
        public string GameAccountId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public OrderDetailsResponse[] OrderProducts { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public decimal TotalSum { get; set; }
    }
}
