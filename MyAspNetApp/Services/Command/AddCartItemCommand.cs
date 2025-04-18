using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Command
{
    public class AddCartItemCommand : ICommand
    {
        private readonly ICartService _cartService;
        private readonly CartItem _cartItem;
        private readonly HttpContext _httpContext;
        private CartItem _previousState;

        public AddCartItemCommand(ICartService cartService, CartItem cartItem, HttpContext httpContext)
        {
            _cartService = cartService;
            _cartItem = cartItem;
            _httpContext = httpContext;
        }

        public async Task Execute()
        {
            var addItem = await _cartService.AddCartItem(_cartItem, _httpContext);
            _previousState = addItem;
        }

        public async Task Undo()
        {
            if(_previousState != null){
                await _cartService.DeleteCartItem(_previousState.Id);
            }
        }
    }

}
