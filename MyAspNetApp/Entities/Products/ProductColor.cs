namespace MyAspNetApp.Entities
{
    public class ProductColor
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Color { get; set; }
            public Product Product { get; set; }
        }
}