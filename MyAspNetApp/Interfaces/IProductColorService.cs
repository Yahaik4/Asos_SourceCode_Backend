using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductColorService
    {
        Task<IEnumerable<ProductColor>> GetAllProductColor();
        Task<ProductColor> CreateProductColor(string name);
        Task<ProductColor> UpdateProductColor(int Id, string name);
        Task<bool> DeleteProduct(int Id);
    }
}