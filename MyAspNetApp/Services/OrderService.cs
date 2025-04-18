using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Services.Observers;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IOrderSubject _orderSubject;
        
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IAddressRepository addressRepository,
            IProductRepository productRepository,IProductVariantRepository productVariantRepository,
            IOrderSubject orderSubject
        )
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
            _orderSubject = orderSubject;
        }

        public async Task<OrderItem> AddOrderItem(AddOrderItemDto addOrderItemDto)
        {
            var existed = await _orderRepository.GetOrderById(addOrderItemDto.OrderId);

            if(existed == null){
                throw new Exception("Order khong ton tai");
            }

            var productVariantExisted = await _productVariantRepository.GetProductVariantById(addOrderItemDto.ProductVariantId);

            if(productVariantExisted == null){
                throw new Exception("ProductVariant khong ton tai");
            }


            var orderItem = new OrderItem{
                OrderId = addOrderItemDto.OrderId,
                ProductVariantId = addOrderItemDto.ProductVariantId,
                Quantity = addOrderItemDto.Quantity,
                Price = addOrderItemDto.Price
            };

            return await _orderRepository.AddOrderItem(orderItem);
        }

        public async Task<Order> CreateOrder(CreateOrderDto orderDto, HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }
            
            var existedAddress = await _addressRepository.GetAddressById(orderDto.AddressId);

            if(existedAddress == null){
                throw new Exception("Dia chi khong ton tai");
            }

            var newOrder = new Order{
                UserId = int.Parse(userId),
                AddressId = orderDto.AddressId,
                TotalPrice = orderDto.TotalPrice,
            };

            return await _orderRepository.CreateOrder(newOrder);
        }

        public async Task<IEnumerable<Object>> GetAllOrder(HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }

            return await _orderRepository.GetAllOrderByUserId(int.Parse(userId));
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if(order == null){
                throw new Exception("Order Khong ton tai");
            }

            return order;
        }

        public async Task<Order> UpdateOrder(UpdateOrderDto order){
            var updatedOrder = await _orderRepository.UpdateOrder(order);

            if (updatedOrder != null)
            {
                await _orderSubject.NotifyAsync(updatedOrder);
            }

            return updatedOrder;
        }
    }

}