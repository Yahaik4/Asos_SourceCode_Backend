using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Object>> GetAllOrder(HttpContext httpContext);
        // Task<CartItemDto> UpdateCartItem(CartItemUpdateDto cartItem);
        // Task<bool> DeleteCartItem(int Id);
        Task<Order> CreateOrder(CreateOrderDto orderDto, HttpContext httpContext);
        Task<OrderItem> AddOrderItem(AddOrderItemDto addOrderItemDto);
        Task<Order> GetOrderById(int orderId);
        Task<Order> UpdateOrder(UpdateOrderDto order);
    }
}