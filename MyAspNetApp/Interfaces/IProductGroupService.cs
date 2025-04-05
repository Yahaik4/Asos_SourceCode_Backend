using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface IProductGroupService
    {
        Task<IEnumerable<ProductGroup>> GetAllProductGroup();
        Task<ProductGroup> CreateProductGroup(ProductGroup productGroup);
        Task<ProductGroup> UpdateProductGroup(ProductGroup productGProductGroup);
        Task<bool> DeleteProduct(int Id);
    }
}