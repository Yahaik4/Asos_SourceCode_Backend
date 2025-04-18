using Microsoft.Extensions.Configuration;
using Stripe;

public sealed class StripeClientSingleton
{
    private static readonly object _lock = new object();
    private static StripeClientSingleton _instance = null;

    public static StripeClientSingleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    throw new Exception("StripeClientSingleton not initialized. Call Initialize() first.");
                }
                return _instance;
            }
        }
    }

    public static void Initialize(IConfiguration configuration)
    {
        lock (_lock)
        {
            if (_instance == null)
            {
                var secretKey = configuration["Stripe:SecretKey"];
                if (string.IsNullOrEmpty(secretKey))
                    throw new Exception("Stripe SecretKey is missing from configuration");

                StripeConfiguration.ApiKey = secretKey;

                _instance = new StripeClientSingleton();
            }
        }
    }
    public PaymentIntentService PaymentIntentService { get; }

    private StripeClientSingleton()
    {
        PaymentIntentService = new PaymentIntentService();
        Console.WriteLine($"StripeClientSingleton instance created: {this.GetHashCode()}"); 
    }
}
