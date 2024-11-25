using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrders.Infrastructure.Data.Repositories;

public class ActivoRepository : IActivoRepository
{
    private readonly ApplicationDbContext _context;

    public ActivoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Activo> GetByIdAsync(int id)
    {
        return await _context.Activos
            .Include(a => a.TipoActivo)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Activo>> GetAllAsync()
    {
        return await _context.Activos
            .Include(a => a.TipoActivo)
            .ToListAsync();
    }
}