namespace MyAspNetApp.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Avatar { get; set; } = "https://example.com/default-avatar.png";
        public string? Password { get; set; }
        public string Role { get; set; } = "customer";

        public List<Address> Addresses { get; set; } = new List<Address>();
        // public List<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();


    }
}