using Microsoft.AspNetCore.Mvc;
using MyAspNetApp.DTOs;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        // api/product
        [HttpGet]
        public async Task<ActionResult> GetAllProduct()
        {
            try
            {
                var products = await _productService.GetAllProduct();

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Get All Product Success",
                    metadata = products
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

        [HttpGet("filter")]
        public async Task<ActionResult> Filter([FromQuery] Filter filter){
            try
            {
                var products = await _productService.Filter(filter);

                return Ok(new
                {
                    statusCode = 200,
                    msg = "Filter Success",
                    metadata = products,
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
        public async Task<ActionResult> CreateProduct([FromQuery] string type, ProductDto productDto)
        {
            if (string.IsNullOrEmpty(type))
            {
                return BadRequest("Type is required");
            }
            try
            {
                
                var newProduct = await _productService.CreateProduct(type, productDto);

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
        public async Task<ActionResult> UpdateProduct(ProductUpdateDto product)
        {
            try
            {
                var updateProduct = await _productService.UpdateProduct(product);

                return Ok(new
                    {
                        statusCode = 200,
                        msg = "update product success",
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            try
            {
                await _productService.DeleteProduct(Id);

                return Ok(new
                {
                    StatusCode = 200,
                    msg = "Delete Success",
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    msg = error.Message
                });
            }
        }

        [HttpGet("{categoryName}")]
        public async Task<ActionResult> GetProductByCategory(string categoryName)
        {
            try
            {
                var products = await _productService.GetProductByCategory(categoryName);

                return Ok(new
                {
                    StatusCode = 200,
                    msg = "Get Product By Category success",
                    metadata = products
                });
            }catch(Exception error)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    msg = error.Message
                });
            }
        }




    }
}