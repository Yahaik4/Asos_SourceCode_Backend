using MyAspNetApp.Entities;

namespace MyAspNetApp.Factories{
    public static class ProductFactory{
        public static Product CreateProduct(string type, string name, decimal price){

            // return type.ToLower() switch 
            // {
            //     "clothing" => new Clothing(name, price, attributes.ContainsKey("material") ? attributes["material"].ToString() : "Unknown"),
            //     "shoes" => new Shoes(name, price, attributes.ContainsKey("soleType") ? attributes["soleType"].ToString() : "Rubber"),
            //     "jewelry" => new Jewelry(name, price, attributes.ContainsKey("metalType") ? attributes["metalType"].ToString() : "Gold"),
            //     _ => throw new ArgumentException("Invalid product type")
            // };
            return type.ToLower() switch 
            {
                "clothing" => new Clothing(name, price),
                "shoes" => new Shoes(name, price),
                "jewelry" => new Jewelry(name, price),
                _ => throw new ArgumentException("Invalid product type")
            };
        }
    }
}