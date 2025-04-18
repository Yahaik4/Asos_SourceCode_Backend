using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Observers
{
    public class NotifyObserver : IOrderObserver
    {
        public Task OnOrderUpdatedAsync(Order order)
        {
            Console.WriteLine($"[Notify] Notification sent for Order {order.Id} with status: {order.Status}");
            return Task.CompletedTask;
        }
    }
}