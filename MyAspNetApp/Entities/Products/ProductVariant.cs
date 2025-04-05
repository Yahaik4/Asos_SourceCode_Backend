using MyAspNetApp.Entities;

public class ProductVariant
{
    public int Id { get; set; }
    
    public int ProductId { get; set; }
    // public Product Product { get; set; } 
    public int SizeId { get; set; }
    public ProductSize Size { get; set; }
    
    public int ColorId { get; set; }
    public ProductColor Color { get; set; }
    
    public int Stock { get; set; }
}
