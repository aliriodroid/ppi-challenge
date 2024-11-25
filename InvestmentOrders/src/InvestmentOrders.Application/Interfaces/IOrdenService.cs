using InvestmentOrders.Application.DTOs;

namespace InvestmentOrders.Application.Interfaces;

public interface IOrdenService
{
    Task<OrdenDto> CrearOrdenAsync(CrearOrdenDto ordenDto);
    Task<OrdenDto?> GetOrdenByIdAsync(int id);
    Task<IEnumerable<OrdenDto>> GetOrdenesAsync();
    Task ActualizarEstadoOrdenAsync(int id, int nuevoEstado);
    Task EliminarOrdenAsync(int id);
}