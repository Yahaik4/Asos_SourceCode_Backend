using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductGroupRepository _productGroupRepository;
        public ProductGroupService(IProductGroupRepository productGroupRepository)
        {
            _productGroupRepository = productGroupRepository;
        }

        public async Task<ProductGroup> CreateProductGroup(ProductGroup productGroup)
        {
            var existed = await _productGroupRepository.GetProductGroupByName(productGroup.Name);

            if(existed != null){
                throw new Exception("Product Group already exists");
            }

            return await _productGroupRepository.CreateProductGroup(productGroup);
        }

        public Task<bool> DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductGroup>> GetAllProductGroup()
        {
            return await _productGroupRepository.GetAllProductGroup();
        }

        public async Task<ProductGroup> UpdateProductGroup(ProductGroup productGroup)
        {
            return await _productGroupRepository.UpdateProductGroup(productGroup) ?? throw new Exception("ProductGroup not exist");
        }
    }

}