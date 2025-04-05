using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductGroupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductGroup> CreateProductGroup(ProductGroup productGroup)
        {
            _context.ProductGroup.Add(productGroup);
            await _context.SaveChangesAsync();
            return productGroup;
        }

        public async Task<bool> DeleteProductGroup(int Id)
        {
            var categories = _context.Categories.Where(c => c.ProductGroupId == Id).ToList();
            _context.Categories.RemoveRange(categories);

            var brands = _context.Brands.Where(b => b.ProductGroupId == Id).ToList();
            _context.Brands.RemoveRange(brands);

            var productProductGroup = await _context.ProductGroup.FindAsync(Id);

            if(productProductGroup != null)
            {
                _context.ProductGroup.Remove(productProductGroup);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;   
        }

        public async Task<IEnumerable<ProductGroup>> GetAllProductGroup()
        {
            return await _context.ProductGroup
                .Include(pg => pg.Categories) 
                .Include(pg => pg.Brands)  
                .Include(pg => pg.ProductSize) 
                .ToListAsync();
        }


        public async Task<ProductGroup> GetProductGroupByName(string name)
        {
            return await _context.ProductGroup.FirstOrDefaultAsync(productGroup => productGroup.Name == name);
        }

        public async Task<ProductGroup> GetProductGroupById(int Id)
        {
            return await _context.ProductGroup.FirstOrDefaultAsync(productGroup => productGroup.Id == Id);
        }

        public async Task<ProductGroup> UpdateProductGroup(ProductGroup productGroup)
        {
            var existed = await _context.ProductGroup
                .Include(pg => pg.Categories)
                .FirstOrDefaultAsync(pg => pg.Id == productGroup.Id);

            if (existed == null)
            {
                return null;
            }

            existed.Name = productGroup.Name;

            foreach (var category in productGroup.Categories)
            {
                if (!existed.Categories.Any(c => c.Id == category.Id))
                {
                    existed.Categories.Add(category);
                }
            }

            _context.ProductGroup.Update(existed);
            await _context.SaveChangesAsync();
            return existed;
        }



    }
}    