using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IOrderObserver
    {
        Task OnOrderUpdatedAsync(Order order);
    }
}