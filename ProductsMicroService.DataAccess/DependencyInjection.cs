using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsMicroService.BusinessLogic.Interfaces;
using ProductsMicroService.DataAccess.Context;

namespace ProductsMicroService.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductsDbContext>(options => options.UseMySQL(configuration.GetConnectionString("MySqlDb")!));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}
