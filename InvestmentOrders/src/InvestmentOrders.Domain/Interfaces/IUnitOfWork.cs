namespace InvestmentOrders.Domain.Interfaces;
public interface IUnitOfWork
{
    IOrdenRepository Ordenes { get; }
    IActivoRepository Activos { get; }
    Task<int> SaveChangesAsync();
}