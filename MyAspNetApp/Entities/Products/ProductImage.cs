namespace MyAspNetApp.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }

        public bool isPrimary { get; set; }
        public int DisplayOrder {get; set; }
        
        // public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}