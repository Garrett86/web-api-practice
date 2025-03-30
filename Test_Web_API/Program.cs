using Microsoft.EntityFrameworkCore;
using Test_Web_API.Entities;
using Test_Web_API.Repositories;
using Test_Web_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//更改 Logger 輸出位置
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


//設定 DbContext 並使用 SQL Server
builder.Services.AddDbContext<NorthwindDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

//註冊 Supplier 模型的資料層和邏輯層到依賴注入容器
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ISupplierDbSeriver, SupplierDbSeriver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
