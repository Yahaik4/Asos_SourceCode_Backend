using MyAspNetApp.Entities;
using MyAspNetApp.Factories;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IProductGroupRepository _productGroupRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IProductVariantRepository _productVariantRepository;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IProductColorRepository _productColorRepository;
        private readonly CloudinaryService _cloudinaryService;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, 
        IProductGroupRepository productGroupRepository, IBrandRepository brandRepository,
        IProductImageRepository productImageRepository,
        IProductVariantRepository productVariantRepository,
        IProductSizeRepository productSizeRepository,
        IProductColorRepository productColorRepository,
        CloudinaryService cloudinaryService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productGroupRepository = productGroupRepository;
            _brandRepository = brandRepository;
            _productImageRepository = productImageRepository;
            _productVariantRepository = productVariantRepository;
            _productColorRepository = productColorRepository;
            _productSizeRepository = productSizeRepository;
            _cloudinaryService = cloudinaryService;
        }

        // public async Task<Product> CreateProduct(string type, string name, decimal price, Dictionary<string, object> attributes)
        // {
        //     if (_productRepository == null)
        //     {
        //         throw new Exception("_productRepository is not initialized.");
        //     }

        //     if (attributes == null)
        //     {
        //         throw new ArgumentException("Attributes cannot be null.");
        //     }
            
        //     var existed = await _productRepository.GetProductByName(name);
            
        //     if(existed != null)
        //     {
        //         throw new Exception("Product already exists");
        //     }

        //     // var category = await _categoryRepository.GetCategoryById(product.CategoryId);
            
        //     // if(category == null)
        //     // {
        //     //     throw new Exception("Category not found");
        //     // }

        //     var newProduct = ProductFactory.CreateProduct(type, name, price, attributes);
        //     // newProduct.Price = product.Price;
        //     // newProduct.Description = product.Description;
        //     // newProduct.Stock = product.Stock;
        //     // newProduct.Gender = product.Gender;
        //     // newProduct.Images = product.Images;
        //     // newProduct.Colors = product.Colors;
        //     // newProduct.CategoryId = product.CategoryId;
        //     // newProduct.BrandId = product.BrandId;

        //     var createProduct = await _productRepository.CreateProduct(newProduct);

        //     return createProduct;
        // }

        public async Task<Product> CreateProduct(string type, ProductDto product)
        {
            var existed = await _productRepository.GetProductByName(product.Name);
            if (existed != null)
            {
                throw new Exception("Product already exists");
            }

            var productType = await _productGroupRepository.GetProductGroupByName(type);
            if (productType == null)
            {
                throw new Exception("ProductType not existed");
            }

            var category = await _categoryRepository.GetCategoryById(product.CategoryId);
            var brand = await _brandRepository.GetBrandById(product.BrandId);
            if (category == null || brand == null)
            {
                throw new Exception("Category or brand not exist for ProductGroup");
            }

            // var attributes = new Dictionary<string, object>();
            // if (product is Clothing clothing) attributes["material"] = clothing.Material;
            // else if (product is Shoes shoes) attributes["soleType"] = shoes.SoleType;
            // else if (product is Jewelry jewelry) attributes["metalType"] = jewelry.MetalType;

            var newProduct = ProductFactory.CreateProduct(type, product.Name, product.Price);

            newProduct.Description = product.Description;
            newProduct.Gender = product.Gender;
            newProduct.Currency = product.Currency;
            newProduct.CategoryId = category.Id;
            // newProduct.Category = category;
            newProduct.BrandId = brand.Id;
            // newProduct.Brand = brand;
            // newProduct.Tags = product.Tags;
            switch (newProduct)
            {
                case Clothing clothing:
                    clothing.Material = product.Material;
                    break;
                case Shoes shoes:
                    shoes.SoleType = product.SoleType;
                    break;
                case Jewelry jewelry:
                    jewelry.MetalType = product.MetalType;
                    break;
            }
            
            await _productRepository.CreateProduct(newProduct);

            // Console.WriteLine(""newProduct.Id);

            if (product.ProductImages?.Any() == true)
            {
                for (int i = 0; i < product.ProductImages.Count; i++)
                {
                    var file = product.ProductImages[i];
                    
                    string folderPath = "Asos/Products/";
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string publicId = folderPath + fileName;

                    using (var stream = file.OpenReadStream())
                    {
                        string imageUrl = await _cloudinaryService.UploadImageAsync(stream, publicId);

                        var img = new ProductImage
                        {
                            ProductId = newProduct.Id,
                            ImageUrl = imageUrl,
                            isPrimary = i == 0,
                            DisplayOrder = i + 1
                        };

                        await _productImageRepository.CreateProductImage(img);
                    }
                }
            }

            try{
                if (product.Variants?.Any() == true)
            {
                int totalStock = 0;
                foreach (var variant in product.Variants)
                {
                    var colorExists = await _productColorRepository.GetProductColorById(variant.ColorId);
                    var sizeExists = await _productSizeRepository.GetProductSizeById(variant.SizeId);

                    if (colorExists == null || sizeExists == null)
                    {
                        throw new Exception("Color or Size does not exist.");
                    }

                    var newVariant = new ProductVariant
                    {
                        ProductId = newProduct.Id,
                        ColorId = variant.ColorId,
                        SizeId = variant.SizeId,
                        Stock = variant.Stock
                    };

                    await _productVariantRepository.CreateProductVariant(newVariant);
                    totalStock += variant.Stock;
                }

                newProduct.Stock = totalStock;
                await _productRepository.UpdateProduct(newProduct);
            }
            }catch(Exception ex){
                throw new Exception($"Error while creating product variant: {ex.Message}", ex);
            }
            
            return newProduct;
        }


        public async Task<bool> DeleteProduct(int Id)
        {
            if(await _productRepository.DeleteProduct(Id) == false)
            {
                throw new Exception("Product not exist");
            }

            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _productRepository.GetAllProduct();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            return await _productRepository.GetProductByCategory(categoryName) ?? throw new Exception("Category not exist");     
        }

        public async Task<Product> UpdateProduct(ProductUpdateDto productDto)
        {
            var existingProduct = await _productRepository.GetProductById(productDto.Id)
                ?? throw new Exception("Product not exists");

            var category = await _categoryRepository.GetCategoryById(productDto.CategoryId)
                ?? throw new Exception("Category not found");

            // Cập nhật thông tin sản phẩm
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Gender = productDto.Gender;
            existingProduct.Currency = productDto.Currency;
            existingProduct.CategoryId = category.Id;
            existingProduct.BrandId = productDto.BrandId;

            // Cập nhật ảnh sản phẩm
            if (productDto.ProductImages != null)
            {
                var existingImages = await _productImageRepository.GetProductImageByProduct(productDto.Id);
                var newImageUrls = productDto.ProductImages.Select(img => img.ImageUrl).ToList();

                foreach (var img in existingImages)
                {
                    if (!newImageUrls.Contains(img.ImageUrl))
                    {
                        await _productImageRepository.DeleteProductImage(img.Id);
                    }
                }

                int displayOrder = 1;
                foreach (var img in productDto.ProductImages)
                {
                    var existingImage = existingImages.FirstOrDefault(i => i.ImageUrl == img.ImageUrl);
                    if (existingImage == null)
                    {
                        await _productImageRepository.CreateProductImage(new ProductImage
                        {
                            ProductId = productDto.Id,
                            ImageUrl = img.ImageUrl,
                            isPrimary = displayOrder == 1,
                            DisplayOrder = displayOrder++
                        });
                    }
                }
            }

            // Cập nhật variants
            if (productDto.Variants != null)
            {
                var existingVariants = await _productVariantRepository.GetProductVariantByProduct(productDto.Id);
                var newVariantKeys = productDto.Variants.Select(v => $"{v.ColorId}-{v.SizeId}").ToHashSet();

                foreach (var variant in existingVariants)
                {
                    string key = $"{variant.ColorId}-{variant.SizeId}";
                    if (!newVariantKeys.Contains(key))
                    {
                        await _productVariantRepository.DeleteProductVariant(variant.Id);
                    }
                }

                int totalStock = 0;
                foreach (var variant in productDto.Variants)
                {
                    var existingVariant = existingVariants.FirstOrDefault(v =>
                        v.ColorId == variant.ColorId && v.SizeId == variant.SizeId);

                    if (existingVariant != null)
                    {
                        existingVariant.Stock = variant.Stock;
                        await _productVariantRepository.UpdateProductVariant(existingVariant);
                    }
                    else
                    {
                        await _productVariantRepository.CreateProductVariant(new ProductVariant
                        {
                            ProductId = productDto.Id,
                            ColorId = variant.ColorId,
                            SizeId = variant.SizeId,
                            Stock = variant.Stock
                        });
                    }

                    totalStock += variant.Stock;
                }

                existingProduct.Stock = totalStock;
            }

            await _productRepository.UpdateProduct(existingProduct);
            return existingProduct;
        }


        public async Task<IEnumerable<Product>> Filter(Filter filter){
            return await _productRepository.Filter(filter);
        }
    }


}