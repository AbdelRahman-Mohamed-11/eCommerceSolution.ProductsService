using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.BusinessLogic.ServiceContracts;
using ProductsMicroService.DataAccess.Context;
using ProductsMicroService.DataAccess.Services;

namespace ProductsMicroService.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var mysqlHost = Environment.GetEnvironmentVariable("MYSQL_HOST");
        var mysqlPassword = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        
        // Debug logging
        Console.WriteLine($"[DEBUG] MYSQL_HOST environment variable: '{mysqlHost}'");
        Console.WriteLine($"[DEBUG] MYSQL_PASSWORD environment variable: '{mysqlPassword}'");
        
        if (string.IsNullOrEmpty(mysqlHost))
        {
            throw new InvalidOperationException("MYSQL_HOST environment variable is not set or is empty. Please ensure it is properly configured.");
        }
        
        if (string.IsNullOrEmpty(mysqlPassword))
        {
            throw new InvalidOperationException("MYSQL_PASSWORD environment variable is not set or is empty. Please ensure it is properly configured.");
        }
        
        var originalConnectionString = configuration.GetConnectionString("MySqlDb");
        Console.WriteLine($"[DEBUG] Original connection string: {originalConnectionString}");
        
        var connectionString = originalConnectionString!
            .Replace("$MYSQL_HOST", mysqlHost)
            .Replace("$MYSQL_PASSWORD", mysqlPassword);

        Console.WriteLine($"[DEBUG] Final connection string: {connectionString}");

        services.AddDbContext<ProductsDbContext>(options => options.UseMySQL(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
