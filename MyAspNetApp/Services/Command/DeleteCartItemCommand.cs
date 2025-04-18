using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Command
{
    public class DeleteCartItemCommand : ICommand
    {
        private readonly ICartService _cartService;
        private readonly int _cartItemId;
        private CartItem _deletedItem;
        private readonly HttpContext _httpContext;

        public DeleteCartItemCommand(ICartService cartService, int cartItemId)
        {
            _cartService = cartService;
            _cartItemId = cartItemId;
        }

        public async Task Execute()
        {
            var cartItem = await _cartService.GetCartItemById(_cartItemId);
            _deletedItem = cartItem;
            await _cartService.DeleteCartItem(_cartItemId);
        }

        public async Task Undo()
        {
            if (_deletedItem != null)
            {
                
                await _cartService.AddCartItem(_deletedItem, _httpContext);
            }
        }
    }

}
