using Microsoft.EntityFrameworkCore;
using MyAspNetApp.Entities;

namespace MyAspNetApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Clothing> Clothings { get; set; }
        public DbSet<Shoes> Shoes { get; set; }
        public DbSet<Jewelry> Jewelries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ProductGroup> ProductGroup { get; set; }
        public DbSet<ProductVariant> ProductVariant { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ Wishlist - User
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany(u => u.Wishlists)
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Giữ cấu hình này nếu cần thiết

            // Cấu hình mối quan hệ WishlistItem - Product (Xóa mối quan hệ với Product)
            // modelBuilder.Entity<WishlistItem>()
            //     .HasOne(wi => wi.Product)
            //     .WithMany() // Không tạo mối quan hệ với Product nữa
            //     .HasForeignKey(wi => wi.ProductId)
            //     .OnDelete(DeleteBehavior.Restrict); // Đảm bảo không có xóa tự động nếu tồn tại WishlistItem

            // Cấu hình mối quan hệ Order - User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Thay Cascade bằng Restrict

            // Cấu hình mối quan hệ Order - Address
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Address)
                .WithMany()
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Restrict); // Thay Cascade bằng Restrict

            // Cấu hình mối quan hệ OrderItem - Order
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Giữ Cascade vì hợp lý (OrderItems phụ thuộc vào Orders)

            // Cấu hình mối quan hệ Cart - User
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Thay Cascade bằng Restrict để tránh xung đột

            // Cấu hình mối quan hệ WishlistItem - Product
            // modelBuilder.Entity<WishlistItem>()
            //     .HasOne(wi => wi.Product)
            //     .WithMany() // Một sản phẩm có thể có nhiều WishlistItem
            //     .HasForeignKey(wi => wi.ProductId)
            //     .OnDelete(DeleteBehavior.Restrict); // Không cho xóa Product nếu còn WishlistItem liên kết

            // Cấu hình mối quan hệ giữa Product và Promotion
            modelBuilder.Entity<ProductPromotion>()
                .HasKey(pp => new { pp.ProductId, pp.PromotionId });

            modelBuilder.Entity<ProductPromotion>()
                .HasOne(pp => pp.Product)
                .WithMany(p => p.ProductPromotions)
                .HasForeignKey(pp => pp.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductPromotion>()
                .HasOne(pp => pp.Promotion)
                .WithMany(p => p.ProductPromotions)
                .HasForeignKey(pp => pp.PromotionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình mối quan hệ phân loại sản phẩm (Discriminator) để xác định kiểu sản phẩm
            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<Clothing>("Clothing")
                .HasValue<Shoes>("Shoe")
                .HasValue<Jewelry>("Jewelry");

            // Các cấu hình khác liên quan đến mối quan hệ Product, Category, Brand, v.v...
            // Đảm bảo rằng không có phần cấu hình nào bị thiếu hoặc bị trùng lặp

            base.OnModelCreating(modelBuilder);
        }
    }
}
