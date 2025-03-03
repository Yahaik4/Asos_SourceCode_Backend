using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;
using MyAspNetApp.Utils;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly Logger _logger;


        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _logger = Logger.GetInstance();
        }


        // API: GET api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllCategory()
        {
            try{
                var categories = await _categoryService.GetAllCategory();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Category Success",
                    metadata =categories
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
        public async Task<ActionResult> CreateCategory(Category category)
        {
            try
            {
                await _categoryService.CreateCategory(category);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Create Category Success",
                    metadata = category
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
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            try
            {
                await _categoryService.UpdateCategory(category);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Update Category Success",
                    metadata = category
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
                await _categoryService.DeleteCategory(Id);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Delete Category Success",
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