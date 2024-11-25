using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Data.Repositories;

public class OrdenRepository : IOrdenRepository
{
    private readonly ApplicationDbContext _context;

    public OrdenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Orden> GetByIdAsync(int id)
    {
        return await _context.Ordenes
            .Include(o => o.Activo)
            .Include(o => o.EstadoOrden)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Orden>> GetAllAsync()
    {
        return await _context.Ordenes
            .Include(o => o.Activo)
            .Include(o => o.EstadoOrden)
            .ToListAsync();
    }

    public async Task<Orden> AddAsync(Orden orden)
    {
        await _context.Ordenes.AddAsync(orden);
        return orden;
    }

    public async Task UpdateAsync(Orden orden)
    {
        _context.Entry(orden).State = EntityState.Modified;
    }

    public async Task DeleteAsync(int id)
    {
        var orden = await _context.Ordenes.FindAsync(id);
        if (orden != null)
            _context.Ordenes.Remove(orden);
    }
}