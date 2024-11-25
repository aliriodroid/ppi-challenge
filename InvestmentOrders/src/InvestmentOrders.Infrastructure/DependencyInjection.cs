using InvestmentOrders.Domain.Interfaces;
using InvestmentOrders.Infrastructure.Data;
using InvestmentOrders.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentOrders.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        
        services.AddScoped<IOrdenRepository, OrdenRepository>();
        services.AddScoped<IActivoRepository, ActivoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}