using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductSizeService
    {
        Task<IEnumerable<ProductSize>> GetAllProductSize();
        Task<ProductSize> CreateProductSize(ProductSize productSize);
        Task<ProductSize> UpdateProductSize(ProductSize productSize);
        Task<bool> DeleteProductSize(int Id);
    }
}