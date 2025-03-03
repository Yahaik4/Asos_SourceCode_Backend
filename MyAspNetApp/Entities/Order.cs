namespace MyAspNetApp.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; }
        public Address Address { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }   
}