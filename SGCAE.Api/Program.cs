
using SGCAE.Application.DependencyInjection;
using SGCAE.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Registrar controladores REST
builder.Services.AddControllers();

// Registrar documentación OpenAPI
builder.Services.AddOpenApi();

// Registrar servicios de Application
builder.Services.AddApplicationServices();

// Registrar servicios de Persistence
builder.Services.AddPersistenceServices(
    builder.Configuration);

var app = builder.Build();

// Habilitar OpenAPI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redireccionar a HTTPS
app.UseHttpsRedirection();

// Habilitar controladores REST
app.MapControllers();

// Iniciar aplicación
app.Run();
