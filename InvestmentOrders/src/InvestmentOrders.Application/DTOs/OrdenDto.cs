namespace InvestmentOrders.Application.DTOs;

public class OrdenDto
{
    public int Id { get; set; }
    public int IdCuenta { get; set; }
    public string NombreActivo { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public char Operacion { get; set; }
    public string DescripcionEstado { get; set; } = string.Empty;
    public decimal MontoTotal { get; set; }
}