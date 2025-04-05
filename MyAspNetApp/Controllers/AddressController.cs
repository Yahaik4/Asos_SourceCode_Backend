using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly Logger _logger;


        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
            _logger = Logger.GetInstance();
        }


        // API: GET api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllAddress()
        {
            try{
                var addresses = await _addressService.GetAllAddress(HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Address Success",
                    metadata = addresses
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
        public async Task<ActionResult> CreateAddress(AddressDto addressDto)
        {
            try
            {
                var address = await _addressService.CreateAddress(addressDto, HttpContext);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create Address Success",
                    metadata = address
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

        // [HttpPut]
        // public async Task<ActionResult> UpdateCategory(Category category)
        // {
        //     try
        //     {
        //         await _categoryService.UpdateCategory(category);

        //         return Ok(new
        //         {
        //             statusCode = 200,
        //             msg = "Update Category Success",
        //             metadata = category
        //         });
        //     }catch(Exception error)
        //     {
        //         return BadRequest(new
        //         {
        //             statusCode = 400,
        //             msg = error.Message
        //         });
        //     }
        // }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteCategory(int Id)
        // {
        //     try{
        //         await _categoryService.DeleteCategory(Id);

        //         return Ok(new
        //         {
        //             statusCode = 200,
        //             msg = "Delete Category Success",
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