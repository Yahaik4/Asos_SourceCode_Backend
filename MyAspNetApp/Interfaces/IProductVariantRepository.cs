using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IProductVariantRepository{
        Task<IEnumerable<ProductVariant>> GetAllProductVariant();
        Task<ProductVariant> GetProductVariantById(int Id);
        Task<IEnumerable<ProductVariant>> GetProductVariantByProduct(int productId);
        Task<ProductVariant> CreateProductVariant(ProductVariant ProductVariant);
        Task<ProductVariant> UpdateProductVariant(ProductVariant ProductVariant);
        Task<bool> DeleteProductVariant(int Id);
    }

}