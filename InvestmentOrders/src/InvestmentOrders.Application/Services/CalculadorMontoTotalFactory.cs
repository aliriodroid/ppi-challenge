using InvestmentOrders.Application.Interfaces;
using InvestmentOrders.Application.Services.Calculadores;
using InvestmentOrders.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentOrders.Application.Services;

public class CalculadorMontoTotalFactory : ICalculadorMontoTotalFactory
{
    private readonly IServiceProvider _serviceProvider;

    public CalculadorMontoTotalFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICalculadorMontoTotal CrearCalculador(int tipoActivoId)
    {
        return tipoActivoId switch
        {
            1 => _serviceProvider.GetRequiredService<CalculadorMontoTotalAccion>(),
            2 => _serviceProvider.GetRequiredService<CalculadorMontoTotalBono>(),
            3 => _serviceProvider.GetRequiredService<CalculadorMontoTotalFCI>(),
            _ => throw new ArgumentException($"Tipo de activo no soportado: {tipoActivoId}")
        };
    }
}