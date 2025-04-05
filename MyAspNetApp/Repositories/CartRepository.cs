using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> findCartItemById(int Id){
            return await _context.CartItem.FirstOrDefaultAsync(cartItem => cartItem.Id == Id);
        }

        public async Task<IEnumerable<CartItemDto>> GetAllCartItem(int cartId)
        {
            return await _context.CartItem
                .Where(c => c.CartId == cartId)
                .Join(_context.ProductVariant,
                    cart => cart.VariantId,
                    variant => variant.Id,
                    (cart, variant) => new { cart, variant })
                .Join(_context.Products,
                    cartVariant => cartVariant.variant.ProductId,
                    product => product.Id,
                    (cartVariant, product) => new CartItemDto
                    {
                        Id = cartVariant.cart.Id,
                        CartId = cartVariant.cart.CartId,
                        VariantId = cartVariant.variant.Id,
                        Color = _context.ProductColors
                                    .Where(c => c.Id == cartVariant.variant.ColorId)
                                    .Select(c => c.Color)
                                    .FirstOrDefault(),
                        Size = _context.ProductSizes
                                    .Where(s => s.Id == cartVariant.variant.SizeId)
                                    .Select(s => s.Size)
                                    .FirstOrDefault(),
                        ProductName = product.Name,
                        ImageUrl = _context.ProductImages
                                    .Where(img => img.ProductId == product.Id)
                                    .Select(img => img.ImageUrl)
                                    .FirstOrDefault(),
                        Quantity = cartVariant.cart.Quantity,
                        Price = cartVariant.cart.Price
                    })
                .ToListAsync();
        }

        public async Task<CartItemDto> GetCartItemDtoById(int cartItemId)
        {
            return await _context.CartItem
                .Where(c => c.Id == cartItemId)
                .Join(_context.ProductVariant,
                    cart => cart.VariantId,
                    variant => variant.Id,
                    (cart, variant) => new { cart, variant })
                .Join(_context.Products,
                    cartVariant => cartVariant.variant.ProductId,
                    product => product.Id,
                    (cartVariant, product) => new CartItemDto
                    {
                        Id = cartVariant.cart.Id,
                        CartId = cartVariant.cart.CartId,
                        VariantId = cartVariant.variant.Id,
                        Color = _context.ProductColors
                                    .Where(c => c.Id == cartVariant.variant.ColorId)
                                    .Select(c => c.Color)
                                    .FirstOrDefault(),
                        Size = _context.ProductSizes
                                    .Where(s => s.Id == cartVariant.variant.SizeId)
                                    .Select(s => s.Size)
                                    .FirstOrDefault(),
                        ProductName = product.Name,
                        ImageUrl = _context.ProductImages
                                    .Where(img => img.ProductId == product.Id)
                                    .Select(img => img.ImageUrl)
                                    .FirstOrDefault(),
                        Quantity = cartVariant.cart.Quantity,
                        Price = cartVariant.cart.Price
                    })
                .FirstOrDefaultAsync();
        }




        public async Task<CartItem> AddCartItem(CartItem cartItem)
        {
            _context.CartItem.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public Task<bool> ClearCart(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Cart> CreateCart(Cart cart)
        {
            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart> GetCartByUserId(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(cart => cart.UserId == userId);
        }

        public async Task<bool> RemoveCartItem(int cartItemId)
        {
            var cartItem = await _context.CartItem.FindAsync(cartItemId);

            if(cartItem != null)
            {
                _context.CartItem.Remove(cartItem);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<CartItem> UpdateCartItem(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> GetCartItemByVariant(int cartId, int variantId)
        {
            return await _context.CartItem.FirstOrDefaultAsync(c => c.CartId == cartId && c.VariantId == variantId);
        }
    }
}