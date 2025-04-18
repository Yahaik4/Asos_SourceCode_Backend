using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WishlistItem>> GetAllWishListItem(int wishlistId){
            var wishlist = await _context.Wishlists
                            .Include(w => w.WishlistItems)
                                .ThenInclude(i => i.product)
                            .FirstOrDefaultAsync(w => w.Id == wishlistId);
            return wishlist?.WishlistItems ?? new List<WishlistItem>();
        }

        public async Task<WishlistItem> AddWishListItem(WishlistItem wishlistItem)
        {
             _context.WishlistItems.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return wishlistItem;
        }

        public async Task<Wishlist> CreateWishlist(Wishlist Wishlist)
        {
            _context.Wishlists.Add(Wishlist);
            await _context.SaveChangesAsync();
            return Wishlist;
        }

        public async Task<bool> DeleteWishListItem(int wishlistItemId)
        {
            var item = await _context.WishlistItems.FindAsync(wishlistItemId);

            if(item != null)
            {
                _context.WishlistItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public Task<Wishlist> GetWishlistById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Wishlist> GetWishlistByUserId(int userId)
        {
            return await _context.Wishlists.FirstOrDefaultAsync(w => w.UserId == userId);
        }
    }
}