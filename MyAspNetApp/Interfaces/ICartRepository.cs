using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserId(int userId);

        Task<CartItem> findCartItemById(int Id);
        Task<Cart> CreateCart(Cart cart);
        Task<IEnumerable<CartItemDto>> GetAllCartItem(int cartId);
        Task<CartItemDto> GetCartItemDtoById(int cartItemId);
        Task<CartItem> AddCartItem(CartItem cartItem);
        Task<CartItem> UpdateCartItem(CartItem cartItem);
        Task<bool> RemoveCartItem(int cartItemId);
        Task<bool> ClearCart(int userId);
        Task<CartItem> GetCartItemByVariant(int cartId, int variantId);
    }
}
