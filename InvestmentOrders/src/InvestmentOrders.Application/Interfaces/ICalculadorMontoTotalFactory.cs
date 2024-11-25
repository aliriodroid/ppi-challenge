using InvestmentOrders.Domain.Services;

namespace InvestmentOrders.Application.Interfaces;

public interface ICalculadorMontoTotalFactory
{
    ICalculadorMontoTotal CrearCalculador(int tipoActivoId);
}