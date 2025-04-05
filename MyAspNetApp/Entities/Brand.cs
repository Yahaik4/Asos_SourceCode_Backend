namespace MyAspNetApp.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int ProductGroupId { get; set; }
        // public List<ProductGroup> ProductGroups { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}