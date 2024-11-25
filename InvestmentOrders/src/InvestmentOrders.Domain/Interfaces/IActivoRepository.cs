using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Domain.Interfaces;

public interface IActivoRepository
{
    Task<Activo> GetByIdAsync(int id);
}