using InvestmentOrders.Domain.Entities;

namespace InvestmentOrders.Domain.Interfaces;

public interface IOrdenRepository
{
    Task<Orden> GetByIdAsync(int id);
    Task<IEnumerable<Orden>> GetAllAsync();
    Task<Orden> AddAsync(Orden orden);
    Task UpdateAsync(Orden orden);
    Task DeleteAsync(int id);
}