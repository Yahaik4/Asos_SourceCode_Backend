using MyAspNetApp.Data;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using MyAspNetApp.Configs;
// using MyAspNetApp.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(); 
builder.Services.AddHttpContextAccessor();

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
