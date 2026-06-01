namespace PizzaGorditosApi.Models;

public class DetallePedido
{
    public int Id { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public int PizzaId { get; set; }
    public Pizza Pizza { get; set; } = null!;

    public string Tamano { get; set; } = "";
    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }
    public decimal SubtotalLinea { get; set; }
}