using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        public BrandService(IBrandRepository brandRepository, IProductGroupRepository productGroupRepository)
        {
            _brandRepository = brandRepository;
            _productGroupRepository = productGroupRepository;
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            var existed = await _brandRepository.GetBrandByName(brand.Name); 

            if (existed != null)
            {
                throw new Exception("Brand already exists");
            }

            var productGroup = await _productGroupRepository.GetProductGroupById(brand.ProductGroupId);

            if(productGroup == null){
                throw new Exception("Product Group not exists");
            }
            
            return await _brandRepository.CreateBrand(brand);
        }

        public async Task<bool> DeleteBrand(int Id)
        {
            if(await _brandRepository.DeleteBrand(Id) == false)
            {
                throw new Exception("Category not exist");
            }

            // var categoryDelete = await _categoryRepository.GetCategoryById(Id);

            // var productGroup = await _productGroupRepository.GetProductGroupByName(categoryDelete.ProductGroupName);

            // productGroup.Categories.Remove(categoryDelete);
            
            return true; 
        }

        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            return await _brandRepository.GetAllBrand();
        }

        public Task<Brand> UpdateBrand(Brand brand)
        {
            throw new NotImplementedException();
        }
    }

}