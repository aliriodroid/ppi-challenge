namespace InvestmentOrders.Application.DTOs;

public class CrearOrdenDto
{
    public int IdCuenta { get; set; }
    public string NombreActivo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public decimal? Precio { get; set; }
    public char Operacion { get; set; }
    public int ActivoId { get; set; }
}