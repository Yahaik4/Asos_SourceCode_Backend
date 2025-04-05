using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductSizeController : ControllerBase
    {
        private readonly IProductSizeService _productSizeService;

        public ProductSizeController(IProductSizeService productSizeService)
        {
            _productSizeService = productSizeService;
        }

        // api/product
        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                var productSizes = await _productSizeService.GetAllProductSize();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Product Size Success",
                    metadat = productSizes
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
        public async Task<ActionResult> CreateProduct(ProductSize productSize)
        {
            try
            {
                var newProduct = await _productSizeService.CreateProductSize(productSize);

                return Ok(new
                    {
                        statusCode = 200,
                        msg = "Create product Size success",
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

        // [HttpPut]
        // public async Task<ActionResult> UpdateProduct(Product product)
        // {
        //     try
        //     {
        //         var updateProduct = await _productService.UpdateProduct(product);

        //         return Ok(new
        //             {
        //                 statusCode = 200,
        //                 msg = "update product success",
        //                 metadata = updateProduct
        //             });
        //     }catch(Exception error)
        //     {
        //         return BadRequest(new
        //         {
        //             statusCode = 400,
        //             msg = error.Message,
        //         });
        //     }
        // }

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