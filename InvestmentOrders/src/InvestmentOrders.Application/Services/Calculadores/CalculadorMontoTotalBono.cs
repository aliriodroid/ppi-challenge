using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Services;

namespace InvestmentOrders.Application.Services.Calculadores;

public class CalculadorMontoTotalBono : ICalculadorMontoTotal
{
    public decimal Calcular(Orden orden, Activo activo)
    {
        decimal montoBase = orden.Cantidad * orden.Precio;
        decimal comision = montoBase * 0.002m;
        decimal impuesto = comision * 0.21m;
        return montoBase + comision + impuesto;
    }
}