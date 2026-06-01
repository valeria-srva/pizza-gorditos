using Microsoft.EntityFrameworkCore;
using PizzaGorditosApi.Data;

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

// Las tablas se crean manualmente en Supabase

app.UseCors("PermitirFrontend");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
