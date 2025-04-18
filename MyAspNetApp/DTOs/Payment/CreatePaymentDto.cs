public class CreatePaymentDto
{
    public int OrderId { get; set; }
    public string PaymentProvider { get; set; }      // "Stripe"
    public string PaymentIntentId { get; set; }      // từ Stripe
    public string TransactionId { get; set; }        // có thể trùng PaymentIntentId hoặc từ Stripe webhook
    public string PaymentMethod { get; set; }        // "Card", "ApplePay", v.v.
    public decimal Amount { get; set; }
    
}
