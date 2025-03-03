using MyAspNetApp.Entities;

namespace MyAspNetApp.Interfaces{

    public interface ICategoryRepository{
        Task<IEnumerable<Category>> GetAllCategory();
        Task<Category> GetCategoryById(int Id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int Id);
    }

}