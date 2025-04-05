namespace MyAspNetApp.Entities
{
    public class ProductTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Tag { get; set; }
        // public Product Product { get; set; }
    }
}