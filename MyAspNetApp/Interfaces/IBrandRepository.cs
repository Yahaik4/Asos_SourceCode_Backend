using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IBrandRepository{
        Task<IEnumerable<Brand>> GetAllBrand();
        Task<Brand> GetBrandById(int Id);
        Task<Brand> GetBrandByName(string name);
        Task<Brand> CreateBrand(Brand brand);
        Task<Brand> UpdateBrand(Brand brand);
        Task<bool> DeleteBrand(int Id);

        Task<Brand> GetBrandByProductGroup(int productGroupId);
    }

}