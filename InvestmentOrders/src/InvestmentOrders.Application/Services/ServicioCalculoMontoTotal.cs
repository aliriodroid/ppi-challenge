using InvestmentOrders.Application.Interfaces;
using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Services;

public class ServicioCalculoMontoTotal : IServicioCalculoMontoTotal
{
    private readonly ICalculadorMontoTotalFactory _calculadorFactory;

    public ServicioCalculoMontoTotal(ICalculadorMontoTotalFactory calculadorFactory)
    {
        _calculadorFactory = calculadorFactory;
    }

    public decimal CalcularMontoTotal(Orden orden, Activo activo)
    {
        var calculador = _calculadorFactory.CrearCalculador(activo.TipoActivoId);
        return calculador.Calcular(orden, activo);
    }
}