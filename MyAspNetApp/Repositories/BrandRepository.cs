using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Data;
using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public BrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public Task<bool> DeleteBrand(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Brand>> GetAllBrand()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task<Brand> GetBrandById(int Id)
        {
            return await _context.Brands.FirstOrDefaultAsync(brand => brand.Id == Id);
        }

        public async Task<Brand> GetBrandByName(string name)
        {
            return await _context.Brands.FirstOrDefaultAsync(brand => brand.Name == name);
        }

        public async Task<Brand> UpdateBrand(Brand brand)
        {
            var existed = await GetBrandById(brand.Id);

            if(existed == null)
            {
                return null;
            }
            
            existed.Name = brand.Name;
            existed.Description = brand.Description;
            
            _context.Brands.Update(existed); 
            
            await _context.SaveChangesAsync();
            return existed;
        }

        public async Task<Brand> GetBrandByProductGroup(int productGroupId)
        {
            return await _context.Brands.FirstOrDefaultAsync(brand => brand.ProductGroupId == productGroupId);
        }
    }
}