using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Services;

namespace InvestmentOrders.Application.Services.Calculadores;

public class CalculadorMontoTotalAccion : ICalculadorMontoTotal
{
    public decimal Calcular(Orden orden, Activo activo)
    {
        decimal montoBase = orden.Cantidad * activo.PrecioUnitario;
        decimal comision = montoBase * 0.006m;
        decimal impuesto = comision * 0.21m;
        return montoBase + comision + impuesto;
    }
}