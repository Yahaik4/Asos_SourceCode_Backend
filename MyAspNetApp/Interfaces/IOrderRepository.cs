using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IOrderRepository{
        Task<Order> GetOrderById(int orderId);
        Task<IEnumerable<object>> GetAllOrderByUserId(int userId);
        Task<Order> CreateOrder(Order order);
        Task<Order> UpdateOrder(UpdateOrderDto order);
        Task<OrderItem> AddOrderItem(OrderItem orderItem);
    }

}