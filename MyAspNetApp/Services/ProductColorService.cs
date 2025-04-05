using MyAspNetApp.Entities;
using MyAspNetApp.Interfaces;

namespace MyAspNetApp.Services
{
    public class ProductColorService : IProductColorService
    {
        private readonly IProductColorRepository _productColorRepository;
        public ProductColorService(IProductColorRepository productColorRepository)
        {
            _productColorRepository = productColorRepository;
        }

        public async Task<ProductColor> CreateProductColor(string name)
        {
            var existed = await _productColorRepository.GetProductColorByName(name); 

            if (existed != null)
            {
                throw new Exception("Color already exists");
            }

            var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "red", "#FF0000" },
                { "blue", "#0000FF" },
                { "green", "#00FF00" },
                { "yellow", "#FFFF00" },
                { "black", "#000000" },
                { "white", "#FFFFFF" }
            };

            string rgb;
            if (!colorMap.TryGetValue(name, out rgb))
            {
                throw new Exception("Invalid color name");
            }

            var productColor = new ProductColor
            {
                Color = name,
                RGB = rgb
            };

            await _productColorRepository.CreateProductColor(productColor); 
            
            return productColor;
        }

        public Task<bool> DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductColor>> GetAllProductColor()
        {
            return _productColorRepository.GetAllProductColor();
        }

        public async Task<ProductColor> UpdateProductColor(int id, string name)
        {
            var existed = await _productColorRepository.GetProductColorById(id);
            
            if (existed == null)
            {
                throw new Exception("Product color not found");
            }

            var duplicate = await _productColorRepository.GetProductColorByName(name);
            if (duplicate != null && duplicate.Id != id)
            {
                throw new Exception("Color already exists");
            }

            var colorMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "red", "#FF0000" },
                { "blue", "#0000FF" },
                { "green", "#00FF00" },
                { "yellow", "#FFFF00" },
                { "black", "#000000" },
                { "white", "#FFFFFF" }
            };

            if (!colorMap.TryGetValue(name, out string rgb))
            {
                throw new Exception("Invalid color name");
            }

            existed.Color = name;
            existed.RGB = rgb;

            await _productColorRepository.UpdateProductColor(existed);

            return existed;
        }

    }

}