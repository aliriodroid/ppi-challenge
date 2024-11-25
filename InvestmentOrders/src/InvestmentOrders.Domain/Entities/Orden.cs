namespace InvestmentOrders.Domain.Entities;

public class Orden
{
    public int Id { get; set; }
    public int IdCuenta { get; set; }
    public string NombreActivo { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public char Operacion { get; set; }
    public int Estado { get; set; }
    public decimal MontoTotal { get; set; }
    public Activo Activo { get; set; }
    public int ActivoId { get; set; }
    public int EstadoId { get; set; }
    public  EstadoOrden EstadoOrden { get; set; } 
}