namespace PizzaGorditosApi.Dtos;

public class CrearPedidoDto
{
    public string Id { get; set; } = "";
    public DateTime At { get; set; }

    public ClienteDto Cliente { get; set; } = new();

    public string Pago { get; set; } = "";

    public decimal Subtotal { get; set; }
    public decimal Descuento { get; set; }
    public decimal Envio { get; set; }
    public decimal Total { get; set; }

    public int Paso { get; set; }

    public List<ItemPedidoDto> Items { get; set; } = new();
}

public class ClienteDto
{
    public string Nombre { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";
    public string Departamento { get; set; } = "";
    public string Municipio { get; set; } = "";
}

public class ItemPedidoDto
{
    public string Sku { get; set; } = "";
    public string Nombre { get; set; } = "";
    public string Tamano { get; set; } = "";
    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }
    public decimal SubtotalLinea { get; set; }
}
