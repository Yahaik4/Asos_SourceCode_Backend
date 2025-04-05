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

        public Task<bool> DeleteBrand(int Id)
        {
            throw new NotImplementedException();
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