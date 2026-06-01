using Microsoft.EntityFrameworkCore;
using PizzaGorditosApi.Models;

namespace PizzaGorditosApi.Data;

public class PizzaGorditosDbContext : DbContext
{
    public PizzaGorditosDbContext(DbContextOptions<PizzaGorditosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Pizza> Pizzas => Set<Pizza>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedido => Set<DetallePedido>();
}
