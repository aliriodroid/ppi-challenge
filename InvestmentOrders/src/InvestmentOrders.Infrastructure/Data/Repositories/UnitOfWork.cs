using InvestmentOrders.Domain.Interfaces;

namespace InvestmentOrders.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IOrdenRepository _ordenRepository;
    private IActivoRepository _activoRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public IOrdenRepository Ordenes => 
        _ordenRepository ??= new OrdenRepository(_context);

    public IActivoRepository Activos =>
        _activoRepository ??= new ActivoRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}