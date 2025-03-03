namespace MyAspNetApp.Entities
{
    public class Clothing : Product
    {
        public string Material { get; set; }
        public Clothing(string name, decimal price, string material) : base(name, price)
        {
            Material = material;
        }

        public override string GetProductType() => "Clothing";
    }
    
}
