namespace MyAspNetApp.Strategies
{
    public class PaymentContext
    {
        private IPaymentStrategy _strategy;

        public void SetStrategy(IPaymentStrategy strategy)
        {
            _strategy = strategy;
        }

        public Task<object> ExecuteStrategy(decimal amount, int orderId)
        {
            return _strategy.CreatePaymentIntent(amount, orderId);
        }
    }
}
