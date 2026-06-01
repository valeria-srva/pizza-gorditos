using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaGorditosApi.Data;
using PizzaGorditosApi.Dtos;
using PizzaGorditosApi.Models;

namespace PizzaGorditosApi.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly PizzaGorditosDbContext db;

    public PedidosController(PizzaGorditosDbContext context)
    {
        db = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearPedido(CrearPedidoDto dto)
    {
        if (dto.Items.Count == 0)
        {
            return BadRequest("El pedido no tiene productos.");
        }

        var usuario = new Usuario
        {
            Nombre = dto.Cliente.Nombre,
            Telefono = dto.Cliente.Telefono,
            Direccion = dto.Cliente.Direccion,
            Departamento = dto.Cliente.Departamento,
            Municipio = dto.Cliente.Municipio
        };

        var pedido = new Pedido
        {
            CodigoPedido = dto.Id,
            FechaPedido = dto.At,
            Usuario = usuario,
            MetodoPago = dto.Pago,
            Subtotal = dto.Subtotal,
            Descuento = dto.Descuento,
            Envio = dto.Envio,
            Total = dto.Total,
            Paso = dto.Paso
        };

        foreach (var item in dto.Items)
        {
            var pizza = await db.Pizzas
                .FirstOrDefaultAsync(p => p.Sku == item.Sku);

            if (pizza == null)
            {
                return BadRequest($"No existe una pizza con SKU: {item.Sku}");
            }

            pedido.Detalles.Add(new DetallePedido
            {
                PizzaId = pizza.Id,
                Tamano = item.Tamano,
                Cantidad = item.Cantidad,
                PrecioUnitario = item.PrecioUnitario,
                SubtotalLinea = item.SubtotalLinea
            });
        }

        db.Pedidos.Add(pedido);
        await db.SaveChangesAsync();

        return Ok(new
        {
            mensaje = "Pedido guardado correctamente",
            pedido.Id,
            pedido.CodigoPedido
        });
    }
}