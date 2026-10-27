
var builder = WebApplication.CreateBuilder(args);

// Registrar servicios para los controladores
builder.Services.AddControllers();

// Habilitar documentación OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Exponer OpenAPI en desarrollo
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Redirigir HTTP a HTTPS
app.UseHttpsRedirection();

// Habilitar los endpoints de los controladores
app.MapControllers();

app.Run();
