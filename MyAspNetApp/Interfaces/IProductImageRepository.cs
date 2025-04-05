using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IProductImageRepository{
        Task<IEnumerable<ProductImage>> GetAllProductImage();
        Task<ProductImage> GetProductImageById(int Id);
        Task<ProductImage> GetProductImageByUrl(string url);
        Task<ProductImage> CreateProductImage(ProductImage productImage);
        Task<ProductImage> UpdateProductImage(ProductImage productImage);
        Task<bool> DeleteProductImage(int Id);

        Task<IEnumerable<ProductImage>> GetProductImageByProduct(int Id);
    }

}