using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IOrderSubject
    {
        void Attach(IOrderObserver observer);
        void Detach(IOrderObserver observer);
        Task NotifyAsync(Order order);
    }
}