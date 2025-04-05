using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductVariantRepository : IProductVariantRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductVariantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductVariant> CreateProductVariant(ProductVariant ProductVariant)
        {
            _context.ProductVariant.Add(ProductVariant);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
            // await _context.SaveChangesAsync();
            Console.WriteLine($"Created ProductVarient ID: {ProductVariant.Id}");
            return ProductVariant;
        }

        public Task<bool> DeleteProductVariant(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductVariant>> GetAllProductVariant()
        {
           return await _context.ProductVariant.ToListAsync();
        }

        public async Task<ProductVariant> GetProductVariantById(int Id)
        {
            return await _context.ProductVariant.FirstOrDefaultAsync(productvariant => productvariant.Id == Id);
        }

        public async Task<IEnumerable<ProductVariant>> GetProductVariantByProduct(int productId)
        {    
            return await _context.ProductVariant
                         .Where(ProductVariant => ProductVariant.ProductId == productId)
                         .ToListAsync();
        }

        public async Task<ProductVariant> UpdateProductVariant(ProductVariant productVariant)
        {
            _context.ProductVariant.Update(productVariant);
            await _context.SaveChangesAsync();
            return productVariant;
        }
    }
}