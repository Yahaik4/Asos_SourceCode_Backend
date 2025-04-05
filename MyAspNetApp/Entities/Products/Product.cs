namespace MyAspNetApp.Entities
{
    public abstract class Product
    {
        public int Id { get; set; } //
        public string Name { get; set; } //
        public string Description { get; set; } 
        public decimal Price { get; set; } //

        public string Gender {get; set; }
        public string Currency { get; set; } = "USD"; 
        public int Stock { get; set; } //

        public int CategoryId { get; set; }  
        // public Category Category { get; set; } 

        public int BrandId { get; set; } 
        // public Brand Brand { get; set; } 

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        // public List<Cart> CartItems { get; set; } = new List<Cart>();
        // public List<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public List<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();

        // public virtual ICollection<ProductColor> Colors { get; set; } = new List<ProductColor>();

        // public List<ProductSize> Sizes { get; set; } = new List<ProductSize>(); 
        public virtual ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductTag> Tags { get; set; } = new List<ProductTag>(); //

        protected Product(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public abstract string GetProductType();
    }   

}