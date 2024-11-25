namespace InvestmentOrders.Domain.Entities;
public class EstadoOrden
{
    public int Id { get; set; }
    public string DescripcionEstado { get; set; }
    public  ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
}