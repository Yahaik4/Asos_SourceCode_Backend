using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductSizeRepository : IProductSizeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductSizeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductSize> CreateProductSize(ProductSize productSize)
        {
            _context.ProductSizes.Add(productSize);
            await _context.SaveChangesAsync();
            return productSize;
        }

        public Task<bool> DeleteProductSize(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductSize>> GetAllProductSize()
        {
            return await _context.ProductSizes.ToListAsync();
        }

        public async Task<ProductSize> GetProductSizeById(int Id)
        {
            return await _context.ProductSizes.FirstOrDefaultAsync(productSize => productSize.Id == Id);
        }

        public async Task<ProductSize> GetProductSizeBySize(string Size)
        {
            return await _context.ProductSizes.FirstOrDefaultAsync(productSize => productSize.Size == Size);
        }

        public async Task<ProductSize> UpdateProductSize(ProductSize productSize)
        {
            var existed = await GetProductSizeById(productSize.Id);

            if(existed == null)
            {
                return null;
            }
            
            existed.Size = productSize.Size;
            existed.ProductGroupId = productSize.ProductGroupId;
            
            _context.ProductSizes.Update(existed); 
            
            await _context.SaveChangesAsync();
            return existed;
        }
    }
}