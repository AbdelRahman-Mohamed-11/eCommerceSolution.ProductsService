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
        services.AddDbContext<ProductsDbContext>(options => options.UseMySQL(configuration.GetConnectionString("MySqlDb")!));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICategoryService, CategoryService>();

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
