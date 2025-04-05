using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductColorController : ControllerBase
    {
        private readonly IProductColorService _productColorService;
        private readonly Logger _logger;


        public ProductColorController(IProductColorService productColorService)
        {
            _productColorService = productColorService;
            _logger = Logger.GetInstance();
        }


        // API: GET api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllProductColor()
        {
            try{
                var productColors = await _productColorService.GetAllProductColor();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Color Success",
                    metadata = productColors
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
        public async Task<ActionResult> CreateProductColor(CreateProductColorDto createProductColorDto)
        {
            try
            {
                var newColor = await _productColorService.CreateProductColor(createProductColorDto.Name);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create productColors Success",
                    metadata = newColor
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
        public async Task<ActionResult> UpdateCategory(UpdateProductColorDto updateProductColorDto)
        {
            try
            {
                await _productColorService.UpdateProductColor(updateProductColorDto.id,updateProductColorDto.Name);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Update COLOR Success",
                    metadata = updateProductColorDto
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