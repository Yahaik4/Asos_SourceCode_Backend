using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface ICartService
    {
        Task<CartItem> GetCartItemById(int Id);
        Task<IEnumerable<CartItemDto>> GetAllCartItem(HttpContext httpContext);
        // Task<CartItem> CreateCartItem(CartItem cartItem, HttpContext httpContext);
        Task<CartItemDto> UpdateCartItem(CartItemUpdateDto cartItem);
        Task<bool> DeleteCartItem(int Id);

        Task<CartItem> AddCartItem(CartItem cartItem, HttpContext httpContext);
    }
}