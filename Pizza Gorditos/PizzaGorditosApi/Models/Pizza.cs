namespace PizzaGorditosApi.Models;

public class Pizza
{
    public int Id { get; set; }

    public string Sku { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Descripcion { get; set; } = "";

    public decimal PrecioPersonal { get; set; }
    public decimal PrecioMediana { get; set; }
    public decimal PrecioFamiliar { get; set; }

    public List<DetallePedido> DetallesPedido { get; set; } = new();
}
