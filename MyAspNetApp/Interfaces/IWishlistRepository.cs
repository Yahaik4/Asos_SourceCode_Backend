using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IWishlistRepository{
        Task<Wishlist> GetWishlistById(int Id);
        Task<Wishlist> GetWishlistByUserId(int UserId);

        Task<IEnumerable<WishlistItem>> GetAllWishListItem(int wishlistId);
        Task<Wishlist> CreateWishlist(Wishlist Wishlist);
        Task<WishlistItem> AddWishListItem(WishlistItem wishlistItem);
        Task<bool> DeleteWishListItem(int wishlistItemId);
        
        // Task<Wishlist> UpdateWishlist(Wishlist Wishlist);
        // Task<bool> DeleteWishlist(int Id);

    }

}