using EasyCommerce.Models;
using EasyCommerce.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register services before building the app
builder.Services.AddControllers(); // Add controllers to the service collection
builder.Services.AddDbContext<EasyCommerceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EasyCommerceConnection"))); // Register DbContext


builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

