using MyAspNetApp.Data;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Builder;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using MyAspNetApp.Configs;
// using MyAspNetApp.DTOs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Cấu hình các dịch vụ cho DI (Dependency Injection)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Cấu hình kết nối database (ví dụ với SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm các service/repository (DI)
builder.Services.AddScoped<MyAspNetApp.Utils.Mailer>();
builder.Services.AddScoped<MyAspNetApp.Utils.Auth>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IUserService, MyAspNetApp.Services.UserService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.IUserRepository, MyAspNetApp.Repositories.UserRepository>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICategoryService, MyAspNetApp.Services.CategoryService>();
builder.Services.AddScoped<MyAspNetApp.Interfaces.ICategoryRepository, MyAspNetApp.Repositories.CategoryRepository>();
// builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductService, MyAspNetApp.Services.ProductService>();
// builder.Services.AddScoped<MyAspNetApp.Interfaces.IProductRepository, MyAspNetApp.Repositories.ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
