using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductGroupRepository
    {
        Task<IEnumerable<ProductGroup>> GetAllProductGroup();
        Task<ProductGroup> GetProductGroupById(int Id);
        Task<ProductGroup> GetProductGroupByName(string name);
        Task<ProductGroup> CreateProductGroup(ProductGroup productGroup);
        Task<ProductGroup> UpdateProductGroup(ProductGroup productGroup);
        Task<bool> DeleteProductGroup(int Id);
    }

    
}