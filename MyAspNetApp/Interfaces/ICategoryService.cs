using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int Id);
    }
}