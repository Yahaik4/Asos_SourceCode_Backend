using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;


namespace MyAspNetApp.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly ApplicationDbContext _context;
        public CartService(ICartRepository cartRepository, IProductRepository productRepository, IProductVariantRepository productVariantRepository, ApplicationDbContext context)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
             _context = context;
        }

        // public async Task<CartItem> CreateCartItem(CartItem cartItem, HttpContext httpContext)
        // {
        //     var userId = Auth.GetUserIdFromToken(httpContext);
        //     if (string.IsNullOrEmpty(userId))
        //     {
        //         throw new Exception("Please Login");
        //     }

        //     var cart = await _cartRepository.GetCartByUserId(int.Parse(userId));
        //     if (cart == null)
        //     {
        //         cart = await _cartRepository.CreateCart(new Cart { UserId = int.Parse(userId) });
        //     }

        //     var productVariant = await _productVariantRepository.GetProductVariantById(cartItem.VariantId);
        //     if (productVariant == null)
        //     {
        //         throw new Exception("Product not found");
        //     }

        //     var product = await _productRepository.GetProductById(productVariant.ProductId);
        //     if (product == null)
        //     {
        //         throw new Exception("Product data not found");
        //     }

        //     var existingCartItem = await _cartRepository.GetCartItemByVariant(cart.Id, cartItem.VariantId);
        //     if (existingCartItem != null)
        //     {
        //         existingCartItem.Quantity += cartItem.Quantity;
        //         existingCartItem.Price = product.Price * existingCartItem.Quantity;  

        //         await _context.SaveChangesAsync(); 
        //         return existingCartItem;
        //     }

        //     cartItem.Price = product.Price * cartItem.Quantity;
        //     cartItem.CartId = cart.Id;

        //     var newCartItem = await _cartRepository.AddCartItem(cartItem);
        //     await _context.SaveChangesAsync(); 

        //     return newCartItem;
        // }



        public async Task<bool> DeleteCartItem(int Id)
        {
            return await _cartRepository.RemoveCartItem(Id);
        }

        public async Task<IEnumerable<CartItemDto>> GetAllCartItem(HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }

            var cart = await _cartRepository.GetCartByUserId(int.Parse(userId));

            if(cart == null){
                return null;
            }
            return await _cartRepository.GetAllCartItem(cart.Id);
        }

        public async Task<CartItemDto> UpdateCartItem(CartItemUpdateDto cartItem)
        {
            var existed = await _cartRepository.findCartItemById(cartItem.Id);

            if(existed == null){
                throw new Exception("CartItem don't existed");
            }

            var unitPrice = existed.Price / existed.Quantity;

            existed.Quantity = cartItem.Quantity;
            existed.Price = unitPrice * cartItem.Quantity;

            await _cartRepository.UpdateCartItem(existed);

            var updatedCartItemDto = await _cartRepository.GetCartItemDtoById(existed.Id);
            
            return updatedCartItemDto; 
        }

        public async Task<CartItem> AddCartItem(CartItem cartItem, HttpContext httpContext)
        {
            var userId = Auth.GetUserIdFromToken(httpContext);
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Please Login");
            }

            var cart = await _cartRepository.GetCartByUserId(int.Parse(userId));
            if (cart == null)
            {
                cart = await _cartRepository.CreateCart(new Cart { UserId = int.Parse(userId) });
            }

            var productVariant = await _productVariantRepository.GetProductVariantById(cartItem.VariantId);
            if (productVariant == null)
            {
                throw new Exception("ProductVariant not found");
            }

            var product = await _productRepository.GetProductById(productVariant.ProductId);
            if (product == null)
            {
                throw new Exception("Product data not found");
            }

            var cartItems = await GetAllCartItem(httpContext);
            if (cartItems != null && cartItems.Any(ci => ci.VariantId == cartItem.VariantId))
            {
                throw new Exception("Product already exists in cart");
            }
 
            cartItem.Price = product.Price;

            cartItem.CartId = cart.Id;
            var addedItem = await _cartRepository.AddCartItem(cartItem);

            return addedItem;
        }

    }

}