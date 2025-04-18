using Stripe;
using MyAspNetApp.Strategies;

public class StripePaymentStrategy : IPaymentStrategy
{
    public async Task<Object> CreatePaymentIntent(decimal amount, int orderId)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = "usd",
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
        };
        var service = StripeClientSingleton.Instance.PaymentIntentService;
        var intent = await service.CreateAsync(options);
        return new
        {
            clientSecret = intent.ClientSecret,
            paymentIntentId = intent.Id
        };
    }
}
