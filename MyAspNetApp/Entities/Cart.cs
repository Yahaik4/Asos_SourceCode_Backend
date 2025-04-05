using System.ComponentModel.DataAnnotations.Schema;

namespace MyAspNetApp.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}