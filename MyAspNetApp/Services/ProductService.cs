// using MyAspNetApp.Entities;
// using MyAspNetApp.Interfaces;

// namespace MyAspNetApp.Services
// {
//     public class ProductService : IProductService
//     {

//         private readonly IProductRepository _productRepository;
//         private readonly ICategoryRepository _categoryRepository;

//         public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
//         {
//             _productRepository = productRepository;
//             _categoryRepository = categoryRepository;
//         }

//         public async Task<Product> CreateProduct(Product product)
//         {
//             var existed = await _productRepository.GetProductByName(product.name);
            
//             if(existed != null)
//             {
//                 throw new Exception("Product already exists");
//             }

//             var category = await _categoryRepository.GetCategoryById(product.CategoryId);
            
//             if(category == null)
//             {
//                 throw new Exception("Category not found");
//             }


//            var newProduct = await _productRepository.CreateProduct(product);

//             return newProduct;
//         }

//         public async Task<bool> DeleteProduct(int Id)
//         {
//             if(await _productRepository.DeleteProduct(Id) == false)
//             {
//                 throw new Exception("Product not exist");
//             }

//             return true;
//         }

//         public async Task<IEnumerable<Product>> GetAllProduct()
//         {
//             return await _productRepository.GetAllProduct();
//         }

//         public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
//         {
//             return await _productRepository.GetProductByCategory(categoryName) ?? throw new Exception("Category not exist");     
//         }

//         public async Task<Product> UpdateProduct(Product product)
//         {
//             var category = await _categoryRepository.GetCategoryById(product.CategoryId);

//             if(category == null)
//             {
//                 throw new Exception("Category not found");
//             }

//             var existed = await _productRepository.GetProductById(product.Id) ?? throw new Exception("Product not exists");


//             return await _productRepository.UpdateProduct(product);
//         }
//     }


// }