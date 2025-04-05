using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductImage> CreateProductImage(ProductImage productImage)
        {
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage;
        }

        public async Task<bool> DeleteProductImage(int Id)
        {
            var image = await _context.ProductImages.FindAsync(Id);
            if (image != null)
            {
                _context.ProductImages.Remove(image);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ProductImage>> GetAllProductImage()
        {
            return await _context.ProductImages.ToListAsync();
        }

        public async Task<ProductImage> GetProductImageByUrl(string url)
        {
            return await _context.ProductImages.FirstOrDefaultAsync(productImage => productImage.ImageUrl == url);
        }

        public async Task<ProductImage> GetProductImageById(int Id)
        {
            return await _context.ProductImages.FirstOrDefaultAsync(productImage => productImage.Id == Id);
        }

        public Task<ProductImage> UpdateProductImage(ProductImage productImage)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductImage>> GetProductImageByProduct(int Id){
            return await _context.ProductImages
                         .Where(productImage => productImage.ProductId == Id)
                         .ToListAsync();
        }
    }
}