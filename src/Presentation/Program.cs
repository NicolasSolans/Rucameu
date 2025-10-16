using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===============================
//  CONFIGURACI�N DEL TOKEN JWT
// ===============================
//var configuration = builder.Configuration;
//var jwtSettings = configuration.GetSection("Jwt");
//var secret = jwtSettings["Secret"];
//var issuer = jwtSettings["Issuer"];
//var audience = jwtSettings["Audience"];

// ===============================
//  SERVICIOS DE LA APLICACI�N
// ===============================
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();

// ===============================
//  CONFIGURACI�N SWAGGER
// ===============================
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Ac� pega el token generado al loguearte"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

//agregamos la configuraci�n para la validaci�n de autenticaci�n
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
    };
}
);

// ===============================
//  CONFIGURACI�N BASE DE DATOS
// ===============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ===============================
//  INYECCI�N DE DEPENDENCIAS
// ===============================
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRepositoryBase<Product>, RepositoryBase<Product>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRepositoryBase<Category>, RepositoryBase<Category>>();
builder.Services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
builder.Services.AddScoped<IRepositoryBase<Client>, RepositoryBase<Client>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRepositoryBase<Admin>, RepositoryBase<Admin>>();
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();

builder.Services.AddAuthorization();

var app = builder.Build();

// ===============================
//  PIPELINE DE EJECUCI�N
// ===============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
