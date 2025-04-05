public class ProductUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Gender { get; set; }
    public string Currency { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public List<ProductImageDto> ProductImages { get; set; }
    public List<ProductVariantDto> Variants { get; set; }
}
