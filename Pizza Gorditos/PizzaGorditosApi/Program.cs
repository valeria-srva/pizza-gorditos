using Microsoft.EntityFrameworkCore;
using PizzaGorditosApi.Data;
using PizzaGorditosApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<PizzaGorditosDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PizzaGorditosConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Crea las tablas y siembra las pizzas iniciales al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PizzaGorditosDbContext>();
    db.Database.ExecuteSqlRaw("DROP TABLE IF EXISTS \"__EFMigrationsHistory\";");
    db.Database.EnsureCreated();

    if (!db.Pizzas.Any())
    {
        db.Pizzas.AddRange(
            new Pizza { Sku = "pep", Nombre = "Pepperoni", Descripcion = "Pepperoni y queso extra", PrecioPersonal = 4, PrecioMediana = 7, PrecioFamiliar = 9 },
            new Pizza { Sku = "mar", Nombre = "Margarita", Descripcion = "Tomate, mozzarella, albahaca", PrecioPersonal = 5, PrecioMediana = 8, PrecioFamiliar = 10 },
            new Pizza { Sku = "4q", Nombre = "4 Quesos", Descripcion = "Mozzarella, gorgonzola, parmesano, ricotta", PrecioPersonal = 6, PrecioMediana = 9, PrecioFamiliar = 11 }
        );
        db.SaveChanges();
    }
}

app.UseCors("PermitirFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
