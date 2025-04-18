using MyAspNetApp.Data;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json.Serialization;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using MyAspNetApp.Configs;
// using MyAspNetApp.DTOs;

var builder = WebApplication.CreateBuilder(args);

StripeClientSingleton.Initialize(builder.Configuration);
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(); 
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<CloudinaryService>();

// builder.Services.AddControllers()
//     .AddJsonOptions(x => 
//         x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// builder.Services.AddHttpContextAccessor();

// Cấu hình các dịch vụ cho DI (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình kết nối database (ví dụ với SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddControllers().AddJsonOptions(options =>
// {
//     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
// });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                .AllowCredentials()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// Thêm các service/repository (DI)
builder.Services.AddScoped<MyAspNetApp.Utils.Mailer>();
builder.Services.AddScoped<MyAspNetApp.Utils.Auth>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IUserService, MyAspNetApp.Services.UserService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IUserRepository, MyAspNetApp.Repositories.UserRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICategoryService, MyAspNetApp.Services.CategoryService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICategoryRepository, MyAspNetApp.Repositories.CategoryRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductService, MyAspNetApp.Services.ProductService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductRepository, MyAspNetApp.Repositories.ProductRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductGroupService, MyAspNetApp.Services.ProductGroupService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductGroupRepository, MyAspNetApp.Repositories.ProductGroupRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IBrandService, MyAspNetApp.Services.BrandService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IBrandRepository, MyAspNetApp.Repositories.BrandRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductSizeService, MyAspNetApp.Services.ProductSizeService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductSizeRepository, MyAspNetApp.Repositories.ProductSizeRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductColorService, MyAspNetApp.Services.ProductColorService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductColorRepository, MyAspNetApp.Repositories.ProductColorRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductImageService, MyAspNetApp.Services.ProductImageService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductImageRepository, MyAspNetApp.Repositories.ProductImageRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductVariantRepository, MyAspNetApp.Repositories.ProductVariantRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IAddressService, MyAspNetApp.Services.AddressService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IAddressRepository, MyAspNetApp.Repositories.AddressRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICartService, MyAspNetApp.Services.CartService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICartRepository, MyAspNetApp.Repositories.CartRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IWishlistService, MyAspNetApp.Services.WishlistService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IWishlistRepository, MyAspNetApp.Repositories.WishlistRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderService, MyAspNetApp.Services.OrderService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderRepository, MyAspNetApp.Repositories.OrderRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IPaymentService, MyAspNetApp.Services.PaymentService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IPaymentRepository, MyAspNetApp.Repositories.PaymentRepository>();

builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderObserver, MyAspNetApp.Services.Observers.SendEmailObserver>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderObserver, MyAspNetApp.Services.Observers.LogObserver>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderObserver, MyAspNetApp.Services.Observers.NotifyObserver>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IOrderSubject, MyAspNetApp.Services.Observers.OrderSubject>();

// builder.Services.AddScoped<MyAspNetApp.Interfaces.ICloudina, MyAspNetApp.Services.PaymentService>();

var app = builder.Build();

app.UseSession();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();


app.Run();
