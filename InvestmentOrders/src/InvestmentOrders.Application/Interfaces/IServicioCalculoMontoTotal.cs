using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Application.Interfaces;

public interface IServicioCalculoMontoTotal
{
    decimal CalcularMontoTotal(Orden orden, Activo activo);
}