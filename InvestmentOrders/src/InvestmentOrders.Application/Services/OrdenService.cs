using AutoMapper;
using InvestmentOrders.Application.DTOs;
using InvestmentOrders.Application.Interfaces;
using InvestmentOrders.Domain.Entities;
using InvestmentOrders.Domain.Interfaces;

public class OrdenService : IOrdenService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IServicioCalculoMontoTotal _servicioCalculoMontoTotal;
    private readonly IMapper _mapper;

    public OrdenService(
        IUnitOfWork unitOfWork,
        IServicioCalculoMontoTotal servicioCalculoMontoTotal,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _servicioCalculoMontoTotal = servicioCalculoMontoTotal;
        _mapper = mapper;
    }

    public async Task<OrdenDto> CrearOrdenAsync(CrearOrdenDto ordenDTO)
    {
        
        var activo = await _unitOfWork.Activos.GetByIdAsync(ordenDTO.ActivoId);
        if (activo == null)
            throw new Exception("Activo no encontrado");

        var orden = _mapper.Map<Orden>(ordenDTO);
        orden.EstadoOrdenId = 0; // Estado inicial "En proceso"
        
        // Si es una acción, usamos el precio de la base de datos
        if (activo.TipoActivoId == 1) // Acción
        {
            orden.Precio = activo.PrecioUnitario;
        }
        
        orden.EstadoOrdenId = 0;
        orden.MontoTotal = _servicioCalculoMontoTotal.CalcularMontoTotal(orden, activo);
        
        await _unitOfWork.Ordenes.AddAsync(orden);
        await _unitOfWork.SaveChangesAsync();

        // Recargar la orden con sus relaciones
        orden = await _unitOfWork.Ordenes.GetByIdAsync(orden.Id);

        var ordenDto = _mapper.Map<OrdenDto>(orden);
        
        return ordenDto;
    }
    public async Task<OrdenDto?> GetOrdenByIdAsync(int id)
    {
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if (orden == null)
            return null;

        return _mapper.Map<OrdenDto>(orden);
    }

    public async Task<IEnumerable<OrdenDto>> GetOrdenesAsync()
    {
        var ordenes = await _unitOfWork.Ordenes.GetAllAsync();
        return _mapper.Map<IEnumerable<OrdenDto>>(ordenes);
    }

    public async Task ActualizarEstadoOrdenAsync(int id, int nuevoEstado)
    {
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if (orden == null)
            throw new KeyNotFoundException($"Orden con ID {id} no encontrada");

        orden.EstadoOrdenId = nuevoEstado;
        await _unitOfWork.Ordenes.UpdateAsync(orden);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EliminarOrdenAsync(int id)
    {
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if (orden == null)
            throw new KeyNotFoundException($"Orden con ID {id} no encontrada");

        await _unitOfWork.Ordenes.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}