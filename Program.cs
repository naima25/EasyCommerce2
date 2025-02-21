using EasyCommerce.Models;
using EasyCommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; 
using EasyCommerce.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EasyCommerce.Services;
using EasyCommerce.Interfaces;


var builder = WebApplication.CreateBuilder(args);

// Register services before building the app
builder.Services.AddControllers(); // Add controllers to the service collection
builder.Services.AddDbContext<EasyCommerceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EasyCommerceConnection"))); // Register DbContext
builder.Services.AddIdentity<Customer, IdentityRole>()
    .AddEntityFrameworkStores<EasyCommerceContext>()
    .AddDefaultTokenProviders();
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<RolesController>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                    });
   



builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

