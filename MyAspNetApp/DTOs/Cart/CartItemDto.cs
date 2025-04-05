public class CartItemDto
{
    public int Id { get; set; }            // ID của CartItem
    public int CartId { get; set; }        // ID của Cart
    public int VariantId { get; set; }     // ID của ProductVariant
    public string Color { get; set; }      // Màu sắc của sản phẩm
    public string Size { get; set; }       // Kích thước của sản phẩm
    public string ProductName { get; set; } // Tên sản phẩm
    public string ImageUrl { get; set; }   // Ảnh của sản phẩm
    public int Quantity { get; set; }      // Số lượng trong giỏ hàng
    public decimal Price { get; set; }     // Giá của sản phẩm
}
