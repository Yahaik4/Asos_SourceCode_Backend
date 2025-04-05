using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProduct();
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        Task<Product> CreateProduct(string type, ProductDto product);
        Task<Product> UpdateProduct(ProductUpdateDto productDto);
        Task<bool> DeleteProduct(int Id);
        Task<IEnumerable<Product>> Filter(Filter filter);

    }
}