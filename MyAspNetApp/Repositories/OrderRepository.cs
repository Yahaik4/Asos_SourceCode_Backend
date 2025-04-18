using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<object>> GetAllOrderByUserId(int userId)
{
    // 1. Lấy orders trước (có ProductVariant nhưng chưa có Product)
    var orders = await _context.Orders
        .Where(o => o.UserId == userId)
        .Include(o => o.User)
        .Include(o => o.Address)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductVariant)
                .ThenInclude(pv => pv.Size)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.ProductVariant)
                .ThenInclude(pv => pv.Color)
        .ToListAsync();

    // 2. Lấy danh sách ProductId
    var productIds = orders
        .SelectMany(o => o.OrderItems)
        .Select(oi => oi.ProductVariant.ProductId)
        .Distinct()
        .ToList();

    // 3. Truy vấn Products theo ID và include ProductImages
    var products = await _context.Products
        .Where(p => productIds.Contains(p.Id))
        .Include(p => p.ProductImages)
        .ToListAsync();

    // 4. Chuyển về dictionary để truy nhanh
    var productDict = products.ToDictionary(p => p.Id);

    // 5. Build kết quả
    var result = orders.Select(o => new
    {
        o.Id,
        o.Status,
        o.TotalPrice,
        o.CreatedAt,
        User = new
        {
            o.User.Name,
            o.User.Email
        },
        Address = o.Address,
        OrderItems = o.OrderItems.Select(oi =>
        {
            var pv = oi.ProductVariant;
            productDict.TryGetValue(pv.ProductId, out var product); // có thể null

            return new
            {
                oi.Id,
                oi.Quantity,
                oi.Price,
                ProductVariant = new
                {
                    Size = pv.Size,
                    Color = pv.Color,
                    ProductName = product?.Name ?? "Unknown",
                    ProductImage = product?.ProductImages?.FirstOrDefault()?.ImageUrl
                }
            };
        })
    });

    return result;
}



        public async Task<Order> UpdateOrder(UpdateOrderDto order)
        {
            var existed = await GetOrderById(order.OrderId);

            if(existed == null)
            {
                return null;
            }
            
            existed.Status = order.Status;
            
            _context.Orders.Update(existed); 
            
            await _context.SaveChangesAsync();
            return existed;
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            return await _context.Orders.FirstOrDefaultAsync(order => order.Id == orderId);
        }

        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
             _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return orderItem;
        }
    }
}