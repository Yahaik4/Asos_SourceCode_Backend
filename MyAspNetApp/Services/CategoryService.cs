using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var existed = await _categoryRepository.GetCategoryByName(category.Name); 

            if (existed != null)
            {
                throw new Exception("Category already exists");
            }
            
            return await _categoryRepository.CreateCategory(category);
            
        }

        public async Task<bool> DeleteCategory(int Id)
        {
            if(await _categoryRepository.DeleteCategory(Id) == false)
            {
                throw new Exception("Category not exist");
            }

            return true;    
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _categoryRepository.GetAllCategory();
        }

        public async Task<Category> UpdateCategory(Category category)
        {   
            return await _categoryRepository.UpdateCategory(category) ?? throw new Exception("Category not exist");
        }
    }

}