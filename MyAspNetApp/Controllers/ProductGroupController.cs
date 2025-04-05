using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupService _productGroupService;

        public ProductGroupController(IProductGroupService productGroupService)
        {
            _productGroupService = productGroupService;
        }


        // api/product
        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                var productGroups = await _productGroupService.GetAllProductGroup();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Product Success",
                    metadata = productGroups
                });
            }catch(Exception error)
            {
                return BadRequest(new
                    {
                        statusCode = 400,
                        msg = error.Message
                    }
                );
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductGroup(ProductGroup productGroup)
        {
            try
            {   
                var newProduct = await _productGroupService.CreateProductGroup(productGroup);

                return Ok(new
                    {
                        statusCode = 200,
                        msg = "Create product success",
                        metadata = newProduct
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductGroup(ProductGroup productGroup)
        {
            try
            {
                var updateProduct = await _productGroupService.UpdateProductGroup(productGroup);

                return Ok(new
                    {
                        statusCode = 200,
                        msg = "update productGroup success",
                        metadata = updateProduct
                    });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    msg = error.Message,
                });
            }
        }

        // [HttpDelete("{id}")]
        // public async Task<ActionResult> DeleteProduct(int Id)
        // {
        //     try
        //     {
        //         await _productService.DeleteProduct(Id);

        //         return Ok(new
        //         {
        //             StatusCode = 200,
        //             msg = "Delete Success",
        //         });
        //     }catch(Exception error)
        //     {
        //         return BadRequest(new
        //         {
        //             StatusCode = 400,
        //             msg = error.Message
        //         });
        //     }
        // }

        // [HttpGet("{categoryName}")]
        // public async Task<ActionResult> GetProductByCategory(string categoryName)
        // {
        //     try
        //     {
        //         var products = await _productService.GetProductByCategory(categoryName);

        //         return Ok(new
        //         {
        //             StatusCode = 200,
        //             msg = "Get Product By Category success",
        //             metadata = products
        //         });
        //     }catch(Exception error)
        //     {
        //         return BadRequest(new
        //         {
        //             StatusCode = 400,
        //             msg = error.Message
        //         });
        //     }
        // }




    }
}