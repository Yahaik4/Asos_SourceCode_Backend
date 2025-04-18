public class CreatePaymentIntentDto
{
    public decimal Amount { get; set; }
    public string Method { get; set; }

    public int OrderId { get; set; }
}
