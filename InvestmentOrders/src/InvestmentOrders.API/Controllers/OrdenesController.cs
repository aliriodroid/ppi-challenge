using FluentValidation;
using InvestmentOrders.Application.DTOs;
using InvestmentOrders.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentOrders.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrdenesController : ControllerBase
{
    private readonly IOrdenService _ordenService;
    private readonly IValidator<CrearOrdenDto> _validator;

    public OrdenesController(IOrdenService ordenService, IValidator<CrearOrdenDto> validator)
    {
        _ordenService = ordenService;
        _validator = validator;
    }

    [HttpPost]
    public async Task<ActionResult<OrdenDto>> CrearOrden(CrearOrdenDto ordenDTO)
    {
        var validationResult = await _validator.ValidateAsync(ordenDTO);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        try
        {
            var orden = await _ordenService.CrearOrdenAsync(ordenDTO);
            return CreatedAtAction(nameof(GetOrden), new { id = orden.Id }, orden);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrdenDto>> GetOrden(int id)
    {
        var orden = await _ordenService.GetOrdenByIdAsync(id);
        if (orden == null)
            return NotFound();

        return orden;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrdenDto>>> GetOrdenes()
    {
        var ordenes = await _ordenService.GetOrdenesAsync();
        return Ok(ordenes);
    }

    [HttpPut("{id}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] int nuevoEstado)
    {
        try
        {
            await _ordenService.ActualizarEstadoOrdenAsync(id, nuevoEstado);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarOrden(int id)
    {
        try
        {
            await _ordenService.EliminarOrdenAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}