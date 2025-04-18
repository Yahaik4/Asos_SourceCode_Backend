using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Observers
{
    public class LogObserver : IOrderObserver
    {
        public Task OnOrderUpdatedAsync(Order order)
        {
            Console.WriteLine($"[Log] Order {order.Id} updated to status: {order.Status}");
            return Task.CompletedTask;
        }
    }
}