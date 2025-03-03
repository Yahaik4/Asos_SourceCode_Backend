namespace MyAspNetApp.Entities
{
    public class Shoes : Product
    {
        public string SoleType { get; set; }
        public Shoes(string name, decimal price, string soleType) : base(name, price)
        {
            SoleType = soleType;
        }

        public override string GetProductType() => "Shoes";
    }

}    