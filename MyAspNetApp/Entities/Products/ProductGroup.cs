namespace MyAspNetApp.Entities
{
    public class ProductGroup
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

            public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();

            public virtual ICollection<ProductSize> ProductSize { get; set; } = new List<ProductSize>();
        }
}