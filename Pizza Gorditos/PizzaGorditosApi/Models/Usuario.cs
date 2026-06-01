namespace PizzaGorditosApi.Models;

public class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = "";
    public string Telefono { get; set; } = "";
    public string Direccion { get; set; } = "";

    public string Departamento { get; set; } = "";
    public string Municipio { get; set; } = "";

    public List<Pedido> Pedidos { get; set; } = new();
}
