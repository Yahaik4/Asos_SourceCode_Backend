public class ProductVariantDto
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public string? Size { get; set; } // ✅ Cho phép null
    public int ColorId { get; set; }
    public string? Color { get; set; } // ✅ Cho phép null
    public int Stock { get; set; }
}
