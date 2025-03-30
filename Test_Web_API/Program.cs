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

//��� Logger ��X��m
builder.Logging.ClearProviders();
builder.Logging.AddConsole();


//�]�w DbContext �èϥ� SQL Server
builder.Services.AddDbContext<NorthwindDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

//���U Supplier �ҫ�����Ƽh�M�޿�h��̿�`�J�e��
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
