using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IBrandService
    {
        Task<IEnumerable<Brand>> GetAllBrand();
        Task<Brand> CreateBrand(Brand brand);
        Task<Brand> UpdateBrand(Brand brand);
        Task<bool> DeleteBrand(int Id);
    }
}