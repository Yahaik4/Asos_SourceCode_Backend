namespace MyAspNetApp.Entities
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal DiscountPercent { get; set; } // Phần trăm giảm giá (0 - 100)
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        public List<ProductPromotion> ProductPromotions { get; set; } = new List<ProductPromotion>();
    }
}