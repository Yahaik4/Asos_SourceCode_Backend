using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductImageController : ControllerBase
    {
        private readonly IProductImageService _productImageService;

        public ProductImageController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // api/product
        [HttpGet]
        public async Task<ActionResult> GetAllProductImage()
        {
            try
            {
                var productImage = await _productImageService.GetAllProductImage();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Product Image Success",
                    metadat = productImage
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
        public async Task<ActionResult> CreateProductImage(ProductImage productImage)
        {
            try
            {
                var newProductImage = await _productImageService.CreateProductImage(productImage);

                return Ok(new
                    {
                        statusCode = 200,
                        msg = "Create product Image success",
                        metadata = newProductImage
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