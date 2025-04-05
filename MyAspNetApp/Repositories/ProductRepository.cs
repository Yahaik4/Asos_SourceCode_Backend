using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> Filter(Filter filter)
        {

            var query = _context.Products
            .Include(p => p.ProductImages)
            .Include(p => p.Variants)
            .AsQueryable();

            if (filter.Id.HasValue)
            {
                query = query
                    .Where(p => p.Id == filter.Id)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Color)
                    .Include(p => p.Variants)
                        .ThenInclude(v => v.Size);
            }

            if (!string.IsNullOrEmpty(filter.Gender))
                query = query.Where(p => p.Gender == filter.Gender);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (filter.BrandId.HasValue)
                query = query.Where(p => p.BrandId == filter.BrandId);

            if (!string.IsNullOrEmpty(filter.CategoryName)){
                var categoryId = await _context.Categories.FirstOrDefaultAsync(category => category.Name == filter.CategoryName);
                query = query.Where(p => p.CategoryId == categoryId.Id);
            }

            if (filter.Price.HasValue)
                query = query.Where(p => p.Price <= filter.Price);

            return await query.ToListAsync();
        }


        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProduct(int Id)
        {
            var product = await _context.Products.FindAsync(Id);

            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products
                .Include(pg => pg.Variants)
                .Include(pg => pg.ProductImages) 
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(category => category.Name == categoryName);

            if(category == null)
            {
                return null;
            }

            var products = await _context.Products
                .Where(p => p.CategoryId == category.Id)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetProductById(int Id)
        {

            return await _context.Products.FirstOrDefaultAsync(Product => Product.Id == Id);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(Product => Product.Name == name);
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            var existed = await GetProductById(product.Id);

            if(existed == null)
            {
                return null;
            }

            existed.Name = product.Name;
            existed.Price = product.Price; 
            existed.Description = product.Description; 
            existed.ProductImages = product.ProductImages; 
            // existed.Colors = product.Colors;
            existed.Gender = product.Gender;
            existed.Stock = product.Stock;
            // existed.Sizes = product.Sizes;
            existed.CategoryId = product.CategoryId;
            existed.BrandId = product.BrandId;
            


            _context.Products.Update(existed); 

            await _context.SaveChangesAsync();
            return existed;
        }
    }
}