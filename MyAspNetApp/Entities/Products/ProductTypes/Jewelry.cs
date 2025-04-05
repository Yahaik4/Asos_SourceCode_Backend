namespace MyAspNetApp.Entities
{
    public class Jewelry : Product
    {
        public string? MetalType { get; set; }
        public Jewelry(string name, decimal price, string? metalType = null) : base(name, price)
        {
            MetalType = metalType;
        }

        public override string GetProductType() {
            return "Jewelry";
        } 
    }
    
}    