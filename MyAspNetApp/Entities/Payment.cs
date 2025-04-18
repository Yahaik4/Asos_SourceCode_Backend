namespace MyAspNetApp.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentProvider { get; set; } // ex: "Stripe"
        public string PaymentIntentId { get; set; } // Stripe PaymentIntent ID
        public string TransactionId { get; set; }   // Optional: Stripe charge ID or similar
        public string Status { get; set; } = "Pending"; // Pending, Succeeded, Failed, Refunded
        public string PaymentMethod { get; set; }   // card, etc.
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PaidAt { get; set; }

        public Order Order { get; set; }
    }
}
