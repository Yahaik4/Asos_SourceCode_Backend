namespace MyAspNetApp.Entities{
    public class WishlistItem
    {
        public int Id { get; set; }
        public int WishlistId { get; set; }
        // public Wishlist Wishlist { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }
    }
}
