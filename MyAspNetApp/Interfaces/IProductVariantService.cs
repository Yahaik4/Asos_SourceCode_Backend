using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductVariantService
    {
        Task<IEnumerable<ProductVariant>> GetAllProductVariant();
        Task<ProductVariant> CreateProductVariant(ProductVariant ProductVariant);
        Task<ProductVariant> UpdateProductVariant(ProductVariant ProductVariant);
        Task<bool> DeleteProductVariant(int Id);
    }
}