using FluentValidation;
using FluentValidation.AspNetCore;
using Lesson_Auth_Integration_Tests.Authorization;
using Lesson_Auth_Integration_Tests.Db;
using Lesson_Auth_Integration_Tests.Interfaces;
<<<<<<< HEAD
using Lesson_Auth_Integration_Tests.Middlewares;
=======
using Lesson_Auth_Integration_Tests.Middleware;
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
using Lesson_Auth_Integration_Tests.Persistence;
using Lesson_Auth_Integration_Tests.Profiles;
using Lesson_Auth_Integration_Tests.Repositories;
using Lesson_Auth_Integration_Tests.Services;
using Lesson_Auth_Integration_Tests.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Lesson_Auth_Integration_Tests;

public class Program
{

    public static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

=======
        // === CONFIGURATION ===
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // === DATABASE (SQLite) ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddScoped<DbInitializer>();

<<<<<<< HEAD
=======
        // === IDENTITY ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 4;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

<<<<<<< HEAD
=======
        // === JWT ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

<<<<<<< HEAD
=======
        // === AUTHORIZATION POLICIES ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("CanCreateTodo", policy =>
                policy.RequireClaim("CanCreateTodo", "true"));

            options.AddPolicy("ApiKeyRequired", policy =>
                policy.Requirements.Add(new ApiKeyRequirement()));
        });

<<<<<<< HEAD
=======
        // === APPLICATION SERVICES (SOLID: Dependency Inversion) ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITodoService, TodoService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

<<<<<<< HEAD
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();

=======
        // === REPOSITORIES ===
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();

        // === MAPPING (AutoMapper) ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddAutoMapper(config =>
        {
            config.AddProfile<MappingProfile>();
        });

<<<<<<< HEAD
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>(ServiceLifetime.Scoped);

        builder.Services.AddSingleton<RatesStore>();
        builder.Services.AddMemoryCache();

=======
        // === VALIDATION (FluentValidation) ===
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>(ServiceLifetime.Scoped);

        // === MIDDLEWARE DEPENDENCIES ===
        builder.Services.AddSingleton<RatesStore>();
        builder.Services.AddMemoryCache();

        // === SWAGGER ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme",
                Name = "X-API-KEY",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

<<<<<<< HEAD
=======
        // === CONTROLLERS ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        builder.Services.AddControllers();

        var app = builder.Build();

<<<<<<< HEAD
=======
        // === DATABASE INITIALIZATION ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
            await dbInitializer.InitializeAsync();
        }

<<<<<<< HEAD
=======
        // === MIDDLEWARE PIPELINE (SOLID: Open/Closed) ===
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

<<<<<<< HEAD
        app.UseRateLimiting();
        app.UseApiKey();
        app.UseRequestLogging();
=======
        // Custom middleware with DI
        app.UseRateLimiting(); // ← Rate limiting before authentication
        app.UseApiKey();       // ← API key validation
        app.UseRequestLogging(); // ← Logging after all auth
>>>>>>> d2042769511955c35cf19f62e6198f8ee90cdd8e

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}