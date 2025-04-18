using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services.Command
{
    public class UpdateCartItemCommand : ICommand
    {
        private readonly ICartService _cartService;
        private readonly CartItemUpdateDto _cartItemUpdateDto;
        private CartItem _previousState;

        public UpdateCartItemCommand(ICartService cartService, CartItemUpdateDto cartItemUpdateDto)
        {
            _cartService = cartService;
            _cartItemUpdateDto = cartItemUpdateDto;
        }

        public async Task Execute()
        {
            var cartItem = await _cartService.GetCartItemById(_cartItemUpdateDto.Id);
            _previousState = new CartItem
            {
                Id = cartItem.Id,
                Quantity = cartItem.Quantity,
                Price = cartItem.Price,
                CartId = cartItem.CartId,
                VariantId = cartItem.VariantId
            };

            await _cartService.UpdateCartItem(_cartItemUpdateDto);
        }

        public async Task Undo()
        {
            if (_previousState != null)
            {
                var restoreDto = new CartItemUpdateDto
                {
                    Id = _previousState.Id,
                    Quantity = _previousState.Quantity,
                };

                await _cartService.UpdateCartItem(restoreDto);
            }
        }
    }

}
