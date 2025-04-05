using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductRepository _productRepository;
        public ProductImageService(IProductImageRepository productImageRepository, IProductRepository productRepository)
        {
            _productImageRepository = productImageRepository;
            _productRepository = productRepository;
        }

        public async Task<ProductImage> CreateProductImage(ProductImage productImage)
        {
            var existed = await _productImageRepository.GetProductImageByUrl(productImage.ImageUrl); 

            if (existed != null)
            {
                throw new Exception("ImageUrl already exists");
            }

            var product = await _productRepository.GetProductById(productImage.ProductId);

            if(product == null){
                throw new Exception("Product not exists");
            }

            var newImage = await _productImageRepository.CreateProductImage(productImage);
            
            return newImage;
        }

        public Task<bool> DeleteProductImage(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductImage>> GetAllProductImage()
        {
            return await _productImageRepository.GetAllProductImage();
        }

        public Task<ProductImage> UpdateProductImage(ProductImage productImage)
        {
            throw new NotImplementedException();
        }
    }

}