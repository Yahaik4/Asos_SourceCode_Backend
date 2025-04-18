namespace MyAspNetApp.Strategies
{
    public interface IPaymentStrategy
    {
        Task<object> CreatePaymentIntent(decimal amount, int orderId);
    }
}
