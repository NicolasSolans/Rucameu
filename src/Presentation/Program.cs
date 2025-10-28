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
//  SERVICIOS DE LA APLICACIÓN
// ===============================
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
builder.Services.AddEndpointsApiExplorer();

// ===============================
//  CONFIGURACIÓN SWAGGER
// ===============================
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pega el token generado al loguearte"
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

//agregamos la configuración para la validación de autenticación
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]!))
    };
}
);
builder.Services.AddAuthorization();


//#region HttpClientFactory
//ApiClientConfiguration apiBResilienceConfiguration = new()
//{
//    RetryCount = 5,
//    RetryAttemptInSeconds = 2,
//    DurationOfBreakInSeconds = 8,
//    HandledEventsAllowedBeforeBreaking = 5
//};

//builder.Services.AddHttpClient("ExternalApiHttpClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7265");
//})
//.AddPolicyHandler(PollyResiliencePolicies.GetRetryPolicy(apiBResilienceConfiguration))
//.AddPolicyHandler(PollyResiliencePolicies.GetCircuitBreakerPolicy(apiBResilienceConfiguration));
//#endregion

// ===============================
//  CONFIGURACIÓN BASE DE DATOS
// ===============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// ===============================
//  INYECCIÓN DE DEPENDENCIAS
// ===============================
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRepositoryBase<Product>, RepositoryBase<Product>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IRepositoryBase<Category>, RepositoryBase<Category>>();
builder.Services.AddScoped<IRepositoryBase<User>, RepositoryBase<User>>();
builder.Services.AddScoped<IRepositoryBase<Client>, RepositoryBase<Client>>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISellPointService, SellPointService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRepositoryBase<Admin>, RepositoryBase<Admin>>();
builder.Services.AddScoped<ICustomAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IRepositoryBase<Employee>, RepositoryBase<Employee>>();
builder.Services.AddScoped<IRepositoryBase<SellPoint>, RepositoryBase<SellPoint>>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IRepositoryBase<ItemCart>, RepositoryBase<ItemCart>>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddScoped<IRepositoryBase<Query>, RepositoryBase<Query>>();
builder.Services.AddScoped<IItemCartRepository, ItemCartRepository>();



var app = builder.Build();

// ===============================
//  PIPELINE DE EJECUCIÓN
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
