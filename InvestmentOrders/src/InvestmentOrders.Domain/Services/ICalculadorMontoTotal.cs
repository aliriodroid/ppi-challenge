using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Domain.Services;

public interface ICalculadorMontoTotal
{
    decimal Calcular(Orden orden, Activo activo);

}