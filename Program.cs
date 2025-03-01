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
using Microsoft.OpenApi.Models;



var builder = WebApplication.CreateBuilder(args);

//Register services before building the app

builder.Services.AddControllers(); // This is an example of dependency Injection (DI)

//Register DbContext with SQL LITE 
//Sets up database connection and allows Entity Framework to handle database operations
//DI will inject the EasyCommerceContext wherever needed
builder.Services.AddDbContext<EasyCommerceContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("EasyCommerceConnection"))); 
    
// Add Identity service with the Customer entity and IdentityRole
// This allows your app to manage user authentication and roles, and DI will
// inject the UserManager and SignInManager wherever needed in the application
builder.Services.AddIdentity<Customer, IdentityRole>()
    .AddEntityFrameworkStores<EasyCommerceContext>()
    .AddDefaultTokenProviders();

//Configure the EmailSettings section from appsettings.json to inject EmailSettings via DI
    builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

 //Scoped means a new instance is created per request
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<RolesController>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();


// Configure JWT token validation parameters (Issuer, Audience, Signing Key, etc.).
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

// Swagger configuration for API documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "E-Commerce API",
        Description = "A simple e-commerce Web API to manage orders, order-items, customers, products, and categories for a coursework project at the University of Westminster.",
        TermsOfService = new Uri("https://www.westminster.ac.uk/terms-of-use"), 
        Contact = new OpenApiContact
        {
            Name = "Naima Abdulle", 
            Url = new Uri("https://example.com/contact") 
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license") 
        }
    });
});


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables the Swagger JSON generation
    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty; 
    });
}

   
app.UseHttpsRedirection();

// Map incoming HTTP requests to their appropriate controllers and actions
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

