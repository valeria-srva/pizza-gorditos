using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaGorditosApi.Data;

namespace PizzaGorditosApi.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly PizzaGorditosDbContext db;

    public DashboardController(PizzaGorditosDbContext context)
    {
        db = context;
    }

    [HttpGet("skus-mas-vendidos")]
    public async Task<IActionResult> SkusMasVendidos()
    {
        var resultado = await db.DetallesPedido
            .GroupBy(d => new
            {
                d.Pizza.Sku,
                d.Pizza.Nombre
            })
            .Select(g => new
            {
                Sku = g.Key.Sku,
                Pizza = g.Key.Nombre,
                CantidadVendida = g.Sum(x => x.Cantidad),
                MontoVendido = g.Sum(x => x.SubtotalLinea)
            })
            .OrderByDescending(x => x.CantidadVendida)
            .ToListAsync();

        return Ok(resultado);
    }

    [HttpGet("ventas-por-zona")]
    public async Task<IActionResult> VentasPorZona()
    {
        var resultado = await db.Pedidos
            .GroupBy(p => new
            {
                p.Usuario.Departamento,
                p.Usuario.Municipio
            })
            .Select(g => new
            {
                Departamento = g.Key.Departamento,
                Municipio = g.Key.Municipio,
                CantidadPedidos = g.Count(),
                TotalVendido = g.Sum(p => p.Total)
            })
            .OrderByDescending(x => x.TotalVendido)
            .ToListAsync();

        return Ok(resultado);
    }

    [HttpGet("gasto-promedio")]
    public async Task<IActionResult> GastoPromedio()
    {
        if (!await db.Pedidos.AnyAsync())
        {
            return Ok(new { GastoPromedio = 0 });
        }

        var resultado = await db.Pedidos
            .AverageAsync(p => p.Total);

        return Ok(new
        {
            GastoPromedio = resultado
        });
    }

    [HttpGet("pizzas-promedio-por-pedido")]
    public async Task<IActionResult> PizzasPromedioPorPedido()
    {
        if (!await db.Pedidos.AnyAsync())
        {
            return Ok(new { PizzasPromedioPorPedido = 0 });
        }

        var resultado = await db.Pedidos
            .Select(p => p.Detalles.Sum(d => d.Cantidad))
            .AverageAsync();

        return Ok(new
        {
            PizzasPromedioPorPedido = resultado
        });
    }

    [HttpGet("ventas-por-fecha")]
    public async Task<IActionResult> VentasPorFecha()
    {
        var resultado = await db.Pedidos
            .GroupBy(p => p.FechaPedido.Date)
            .Select(g => new
            {
                Fecha = g.Key,
                CantidadPedidos = g.Count(),
                TotalVendido = g.Sum(p => p.Total)
            })
            .OrderBy(x => x.Fecha)
            .ToListAsync();

        return Ok(resultado);
    }
}
