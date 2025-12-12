using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
//using RealEstate.Core.Interfaces;
using RealEstate.Core.Services;
using RealEstate.Infrastructure;
//using RealEstate.Infrastructure.Repositories;
using RealEstate.Infrastructure.Services;
using RealEstate.Core.Entities;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<RealEstateDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Session & Caching
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton(x =>
{
    var config = builder.Configuration.GetSection("Cloudinary").Get<CloudinarySettings>();
    var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new Cloudinary(account);
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextJs", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Build app
var app = builder.Build();

// Middleware
app.UseSession();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors("AllowNextJs");
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Run
app.Run();
