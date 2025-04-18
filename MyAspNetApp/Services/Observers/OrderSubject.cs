using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Observers
{
    public class OrderSubject : IOrderSubject
    {
        private readonly List<IOrderObserver> _observers;

        public OrderSubject(IEnumerable<IOrderObserver> observers)
        {
            _observers = observers.ToList();
        }

        public Task NotifyAsync(Order order)
        {
            return Task.WhenAll(_observers.Select(o => o.OnOrderUpdatedAsync(order)));
        }

        public void Attach(IOrderObserver observer) => _observers.Add(observer);
        public void Detach(IOrderObserver observer) => _observers.Remove(observer);
    }
}