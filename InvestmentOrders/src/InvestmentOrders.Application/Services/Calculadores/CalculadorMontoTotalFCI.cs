using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Services;

namespace InvestmentOrders.Application.Services.Calculadores;

public class CalculadorMontoTotalFCI : ICalculadorMontoTotal
{
    public decimal Calcular(Orden orden, Activo activo)
    {
        return orden.Cantidad * orden.Precio;
    }
}