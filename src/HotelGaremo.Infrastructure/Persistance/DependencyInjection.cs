using HotelGaremo.Application.Abstraction;
using HotelGaremo.Application.Interfaces;
using HotelGaremo.Infrastructure.Persistance;
using HotelGaremo.Infrastructure.Services;
using HotelGaremo.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelGaremo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IDataContext, AppDbContext>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IImageService, ImageService>();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings")); 
        services.AddScoped<IJwtService, JwtService>(); 
        return services;
    }
}