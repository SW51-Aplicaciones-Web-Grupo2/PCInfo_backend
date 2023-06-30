using System.Text;
using Microsoft.EntityFrameworkCore;
using PCInfo_backend.PCInfo.Domain.Repositories;
using PCInfo_backend.PCInfo.Domain.Services;
using PCInfo_backend.PCInfo.Mapping;
using PCInfo_backend.PCInfo.Persistence.Contexts;
using PCInfo_backend.PCInfo.Persistence.Repositories;
using PCInfo_backend.PCInfo.Services;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PCInfo_backend.PCInfo.Security.Authorization.Handlers.Implementations;
using PCInfo_backend.PCInfo.Security.Authorization.Handlers.Interfaces;
using PCInfo_backend.PCInfo.Security.Authorization.Middleware;
using PCInfo_backend.PCInfo.Security.Authorization.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration


builder.Services.AddScoped<IRamRepository, RamRepository>();
builder.Services.AddScoped<IRamService, RamService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<PCInfo_backend.PCInfo.Security.Domain.Repositories.IUserRepository, PCInfo_backend.PCInfo.Security.Persistence.Repositories.UserRepository>();
builder.Services.AddScoped<PCInfo_backend.PCInfo.Security.Domain.Services.IUserService, PCInfo_backend.PCInfo.Security.Services.UserService>();

// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile),
    typeof(PCInfo_backend.PCInfo.Security.Mapping.ModelToResourceProfile),
    typeof(PCInfo_backend.PCInfo.Security.Mapping.ModelToResourceProfile));

// CORS Configuration
builder.Services.AddCors();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
{
// Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
});


// Add JWT Authentication

var key = Encoding.ASCII.GetBytes(builder.Configuration["JWT:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddSwaggerGen(options =>
{
// Add API Documentation Information
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
});

var app = builder.Build();

// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
        context.Database.EnsureCreated(); 
}

app.UseCors("AllowAllOrigins");

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();
// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();