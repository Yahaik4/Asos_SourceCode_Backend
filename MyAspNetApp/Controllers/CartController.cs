using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly Logger _logger;


        public CartController(ICartService cartService)
        {
            _cartService = cartService;
            _logger = Logger.GetInstance();
        }


        [HttpGet]
        public async Task<ActionResult> GetAllCart()
        {
            try{
                var cartItems = await _cartService.GetAllCartItem(HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All CartItem Success",
                    metadata = cartItems
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCartItem(CartItem cartItem)
        {
            try
            {
                await _cartService.AddCartItem(cartItem, HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create CartItem Success",
                    metadata = cartItem
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(CartItemUpdateDto cartItem)
        {
            try
            {
                var updatedItem = await _cartService.UpdateCartItem(cartItem);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Update Category Success",
                    metadata = updatedItem
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int Id)
        {
            try{
                await _cartService.DeleteCartItem(Id);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Delete CartItem Success",
                    metadata = true
                });
            }catch(Exception error){


                _logger.Log($"Error fetching categories: {error.Message}");
                
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message
                });
            }
        }

    }
}