using MyAspNetApp.Entities;

public class GetProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
    public string Gender { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    // public List<ProductTag> Tags { get; set; }
    // public Dictionary<string, string> Attributes { get; set; }
    public string ProductType { get; set; }
    public string? Material { get; set; }
    public string? MetalType{ get; set; }
    public string? SoleType{ get; set; }
    public List<string> ImageUrls { get; set; }
    public List<ProductVariant> Variants { get; set; }
}