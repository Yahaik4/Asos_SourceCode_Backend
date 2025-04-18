public class AddOrderItemDto{
    public int OrderId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}