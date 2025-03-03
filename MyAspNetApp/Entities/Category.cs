namespace MyAspNetApp.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }

        public List<Product> Products { get; set; } = new();
    }
}
