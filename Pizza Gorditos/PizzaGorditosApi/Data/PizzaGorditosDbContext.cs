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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pizza>().HasData(
            new Pizza
            {
                Id = 1,
                Sku = "pep",
                Nombre = "Pepperoni",
                Descripcion = "Pepperoni y queso extra",
                PrecioPersonal = 4,
                PrecioMediana = 7,
                PrecioFamiliar = 9
            },
            new Pizza
            {
                Id = 2,
                Sku = "mar",
                Nombre = "Margarita",
                Descripcion = "Tomate, mozzarella, albahaca",
                PrecioPersonal = 5,
                PrecioMediana = 8,
                PrecioFamiliar = 10
            },
            new Pizza
            {
                Id = 3,
                Sku = "4q",
                Nombre = "4 Quesos",
                Descripcion = "Mozzarella, gorgonzola, parmesano, ricotta",
                PrecioPersonal = 6,
                PrecioMediana = 9,
                PrecioFamiliar = 11
            }
        );
    }
}