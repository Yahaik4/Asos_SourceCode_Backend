using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        public CategoryService(ICategoryRepository categoryRepository, IProductGroupRepository productGroupRepository)
        {
            _categoryRepository = categoryRepository;
            _productGroupRepository = productGroupRepository;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            var existed = await _categoryRepository.GetCategoryByName(category.Name); 

            if (existed != null)
            {
                throw new Exception("Category already exists");
            }

            var productGroup = await _productGroupRepository.GetProductGroupById(category.ProductGroupId);

            if(productGroup == null){
                throw new Exception("Product Group not exists");
            }

            var newCategory = await _categoryRepository.CreateCategory(category);
            
            return newCategory;
            
        }

        public async Task<bool> DeleteCategory(int Id)
        {

            if(await _categoryRepository.DeleteCategory(Id) == false)
            {
                throw new Exception("Category not exist");
            }

            // var categoryDelete = await _categoryRepository.GetCategoryById(Id);

            // var productGroup = await _productGroupRepository.GetProductGroupByName(categoryDelete.ProductGroupName);

            // productGroup.Categories.Remove(categoryDelete);
            
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