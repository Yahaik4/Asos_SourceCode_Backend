using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly Logger _logger;


        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
            _logger = Logger.GetInstance();
        }


        // API: GET api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllBrand()
        {
            try{
                var brands = await _brandService.GetAllBrand();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Brand Success",
                    metadata = brands
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateBrand(Brand brand)
        {
            try
            {
                await _brandService.CreateBrand(brand);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create Brand Success",
                    metadata = brand
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