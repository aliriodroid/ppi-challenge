using System.Reflection;
using FluentValidation;
using InvestmentOrders.Application.Interfaces;
using InvestmentOrders.Application.Services;
using InvestmentOrders.Application.Services.Calculadores;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentOrders.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddScoped<CalculadorMontoTotalAccion>();
        services.AddScoped<CalculadorMontoTotalBono>();
        services.AddScoped<CalculadorMontoTotalFCI>();
        services.AddScoped<ICalculadorMontoTotalFactory, CalculadorMontoTotalFactory>();
        services.AddScoped<IServicioCalculoMontoTotal, ServicioCalculoMontoTotal>();
        services.AddScoped<IOrdenService, OrdenService>();
        return services;
    }
}