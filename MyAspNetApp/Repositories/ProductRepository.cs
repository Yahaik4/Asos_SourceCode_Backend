// using Microsoft.EntityFrameworkCore;
// using MyAspNetApp.Data;
// using MyAspNetApp.Entities;
// using MyAspNetApp.Interfaces;

// namespace MyAspNetApp.Repositories
// {
//     public class ProductRepository : IProductRepository
//     {
//         private readonly ApplicationDbContext _context;

//         public ProductRepository(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         public async Task<Product> CreateProduct(Product product)
//         {
//             _context.Products.Add(product);
//             await _context.SaveChangesAsync();
//             return product;
//         }

//         public async Task<bool> DeleteProduct(int Id)
//         {
//             var product = await _context.Products.FindAsync(Id);

//             if(product != null)
//             {
//                 _context.Products.Remove(product);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             return false;
//         }

//         public async Task<IEnumerable<Product>> GetAllProduct()
//         {
//             return await _context.Products.ToListAsync();
//         }

//         public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
//         {
//             var category = await _context.Categories.FirstOrDefaultAsync(category => category.name == categoryName);

//             if(category == null)
//             {
//                 return null;
//             }

//             var products = await _context.Products
//                 .Where(p => p.CategoryId == category.Id)
//                 .ToListAsync();

//             return products;
//         }

//         public async Task<Product> GetProductById(int Id)
//         {

//             return await _context.Products.FirstOrDefaultAsync(Product => Product.Id == Id);
//         }

//         public async Task<Product> GetProductByName(string name)
//         {
//             return await _context.Products.FirstOrDefaultAsync(Product => Product.name == name);
//         }

//         public async Task<Product> UpdateProduct(Product product)
//         {
//             var existed = await GetProductById(product.Id);

//             if(existed == null)
//             {
//                 return null;
//             }

//             existed.name = product.name;
//             existed.originalPrice = product.originalPrice; 
//             existed.sellPrice = product.sellPrice; 
//             existed.description = product.description; 
//             existed.image_url = product.image_url; 
//             existed.coulors = product.coulors;
//             existed.gender = product.gender;
//             existed.quantity = product.quantity;
//             existed.sizes = product.sizes;
//             existed.isAvailable = product.isAvailable; 
//             existed.CategoryId = product.CategoryId;

//             _context.Products.Update(existed); 

//             await _context.SaveChangesAsync();
//             return existed;
//         }
//     }
// }