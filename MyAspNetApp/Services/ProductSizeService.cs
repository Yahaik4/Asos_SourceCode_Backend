using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        public ProductSizeService(IProductSizeRepository productSizeRepository, IProductGroupRepository productGroupRepository)
        {
            _productSizeRepository = productSizeRepository;
            _productGroupRepository = productGroupRepository;
        }

        public async Task<ProductSize> CreateProductSize(ProductSize productSize)
        {
            var existed = await _productSizeRepository.GetProductSizeBySize(productSize.Size); 

            if (existed != null)
            {
                throw new Exception("ProductSize already exists");
            }

            var productGroup = await _productGroupRepository.GetProductGroupById(productSize.ProductGroupId);

            if(productGroup == null){
                throw new Exception("Product Group not exists");
            }
            
            return await _productSizeRepository.CreateProductSize(productSize);
        }

        public Task<bool> DeleteProductSize(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductSize>> GetAllProductSize()
        {
            return await _productSizeRepository.GetAllProductSize();
        }

        public Task<ProductSize> UpdateProductSize(ProductSize productSize)
        {
            throw new NotImplementedException();
        }
    }

}