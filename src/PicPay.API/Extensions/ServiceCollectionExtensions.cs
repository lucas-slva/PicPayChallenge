using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PicPay.API.Middlewares;
using PicPay.Application.Interfaces;
using PicPay.Application.Services;
using PicPay.Application.Validators;
using PicPay.Domain.Interfaces;
using PicPay.Infrastructure.Data;
using PicPay.Infrastructure.Repositories;

namespace PicPay.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        // JWT Configuration
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"] ?? throw new InvalidOperationException("JWT Secret Key is missing"));
        
        // Db connection
        builder.Services.AddDbContext<PicPayDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    
        // Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IWalletRepository, WalletRepository>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
    
        // Services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IWalletService, WalletService>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
    
        // FluentValidation
        builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<WalletValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<TransactionValidator>();
    
        // Middleware
        builder.Services.AddTransient<ExceptionHandlingMiddleware>();
    
        // Controllers
        builder.Services.AddControllers();
    
        // Authentication
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
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
    }
}