using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IWishlistService
    {
        Task<IEnumerable<WishlistItem>> GetAllWishlistItem(HttpContext httpContext);
        Task<WishlistItem> AddWishlistItem(WishlistItem wishlistItem, HttpContext httpContext);
        Task<bool> DeleteWishlist(int Id);

        
    }
}