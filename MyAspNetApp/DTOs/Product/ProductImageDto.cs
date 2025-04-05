public class ProductImageDto
{
    public int id { get; set; }
    public int ProductId { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }
}
