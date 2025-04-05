namespace MyAspNetApp.Entities
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int ProductGroupId { get; set; }

        // public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        // public ICollection<ProductVariant> ProductVariants { get; set; } 
    }
}