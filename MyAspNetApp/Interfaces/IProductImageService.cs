using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductImageService
    {
        Task<IEnumerable<ProductImage>> GetAllProductImage();
        Task<ProductImage> CreateProductImage(ProductImage productImage);
        Task<ProductImage> UpdateProductImage(ProductImage productImage);
        Task<bool> DeleteProductImage(int Id);
    }
}