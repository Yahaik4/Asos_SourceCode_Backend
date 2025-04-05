using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;
        private readonly Logger _logger;


        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
            _logger = Logger.GetInstance();
        }


        [HttpGet]
        public async Task<ActionResult> GetAllWishListItem()
        {
            try{
                var wishlistItem = await _wishlistService.GetAllWishlistItem(HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All CartItem Success",
                    metadata = wishlistItem
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
        public async Task<ActionResult> AddCartItem(WishlistItem wishlistItem)
        {
            try
            {
                await _wishlistService.AddWishlistItem(wishlistItem, HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create wishlistItem Success",
                    metadata = wishlistItem
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
        

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteCategory(int Id)
        // {
        //     try{
        //         await _cartService.DeleteCartItem(Id);

        //         return Ok(new
        //         {
        //             statusCode = 200,
        //             msg = "Delete CartItem Success",
        //             metadata = true
        //         });
        //     }catch(Exception error){


        //         _logger.Log($"Error fetching categories: {error.Message}");
                
        //         return BadRequest(new
        //         {
        //             statusCode = 400,
        //             msg = error.Message
        //         });
        //     }
        // }

    }
}