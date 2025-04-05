using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IProductSizeRepository{
        Task<IEnumerable<ProductSize>> GetAllProductSize();
        Task<ProductSize> GetProductSizeById(int Id);
        Task<ProductSize> GetProductSizeBySize(string Size);
        Task<ProductSize> CreateProductSize(ProductSize ProductSize);
        Task<ProductSize> UpdateProductSize(ProductSize ProductSize);
        Task<bool> DeleteProductSize(int Id);
    }

}