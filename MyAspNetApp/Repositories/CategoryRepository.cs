using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteCategory(int Id)
        { 
            var category = await _context.Categories.FindAsync(Id);

            if(category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            return await _context.Categories.FirstOrDefaultAsync(category => category.Id == Id);
        }

        public async Task<Category> GetCategoryByName(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(category => category.Name == name);
        }

        public async Task<Category> UpdateCategory(Category category)
        {
            var existed = await GetCategoryById(category.Id);

            if(existed == null)
            {
                return null;
            }
            
            existed.Name = category.Name;
            
            _context.Categories.Update(existed); 
            
            await _context.SaveChangesAsync();
            return existed;
        }
    }
}