using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductColorRepository : IProductColorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductColorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductColor> CreateProductColor(ProductColor productColor)
        {
            _context.ProductColors.Add(productColor);
            await _context.SaveChangesAsync();
            return productColor;
        }

        public async Task<bool> DeleteProductColor(int Id)
        {
            var color = await _context.ProductColors.FindAsync(Id);

            if(color != null)
            {
                _context.ProductColors.Remove(color);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProductColor>> GetAllProductColor()
        {
            return await _context.ProductColors.ToListAsync();
        }

        public async Task<ProductColor> GetProductColorById(int Id)
        {
            return await _context.ProductColors.FirstOrDefaultAsync(ProductColor => ProductColor.Id == Id);
        }

        public async Task<ProductColor> GetProductColorByName(string name)
        {
            return await _context.ProductColors.FirstOrDefaultAsync(ProductColor => ProductColor.Color == name);
        }

        public async Task<ProductColor> UpdateProductColor(ProductColor productColor)
        {
            var existed = await GetProductColorById(productColor.Id);

            if(existed == null)
            {
                return null;
            }
            
            existed.Color = productColor.Color;
            existed.RGB = productColor.RGB;
            
            _context.ProductColors.Update(existed); 
            
            await _context.SaveChangesAsync();
            return existed;
        }
    }
}