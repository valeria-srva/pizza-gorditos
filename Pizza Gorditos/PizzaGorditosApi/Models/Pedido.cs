namespace PizzaGorditosApi.Models;

public class Pedido
{
    public int Id { get; set; }

    public string CodigoPedido { get; set; } = "";
    public DateTime FechaPedido { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public string MetodoPago { get; set; } = "";

    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Envio { get; set; }
    public decimal Total { get; set; }

    public int Paso { get; set; }

    public List<DetallePedido> Detalles { get; set; } = new();
}
