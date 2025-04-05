using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface IProductColorRepository{
        Task<IEnumerable<ProductColor>> GetAllProductColor();
        Task<ProductColor> GetProductColorById(int Id);
        Task<ProductColor> GetProductColorByName(string name);
        Task<ProductColor> CreateProductColor(ProductColor productColor);
        Task<ProductColor> UpdateProductColor(ProductColor productColor);
        Task<bool> DeleteProductColor(int Id);
    }

}