

using MyAspNetApp.Entities;

namespace MyAspNetApp.Entities
{
    public class ProductColor
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public string RGB { get; set; }  

        // public ICollection<ProductVariant> ProductVariants { get; set; } 
    }
}