using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IProductRepository _productRepository;
        
        public WishlistService(IWishlistRepository wishlistRepository, IProductRepository productRepository)
        {
            _wishlistRepository = wishlistRepository;
            _productRepository = productRepository;
        }

        public async Task<WishlistItem> AddWishlistItem(WishlistItem wishlistItem, HttpContext httpContext)
        {   

            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }

            var wishlist = await _wishlistRepository.GetWishlistByUserId(int.Parse(userId));
            if (wishlist == null)
            {
                wishlist = await _wishlistRepository.CreateWishlist(new Wishlist { UserId = int.Parse(userId) });
            }

            var product = await _productRepository.GetProductById(wishlistItem.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var wishlistItems = await GetAllWishlistItem(httpContext);
            if (wishlistItems != null && wishlistItems.Any(ci => ci.ProductId == wishlistItem.ProductId))
            {
                throw new Exception("Product already exists in wishList");
            }

            wishlistItem.WishlistId = wishlist.Id;
            var addedItem = await _wishlistRepository.AddWishListItem(wishlistItem);

            return wishlistItem;
        }

        public Task<Wishlist> CreateWishlist(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteWishlist(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WishlistItem>> GetAllWishlistItem(HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }

            var wishlist = await _wishlistRepository.GetWishlistByUserId(int.Parse(userId));

            if(wishlist == null){
                throw new Exception("Please Add new Item");
            }
            return await _wishlistRepository.GetAllWishListItem(wishlist.Id);
        }

    }

}