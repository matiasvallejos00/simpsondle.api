using SimpsonsDle.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS ---

// Registramos el servicio de personajes
builder.Services.AddSingleton<CharacterService>();

// Configuración de CORS para que Vercel pueda entrar sin bloqueos
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

var app = builder.Build();

// --- 2. CONFIGURACIÓN DEL PIPELINE ---

app.UseHttpsRedirection();

// Fundamental para que las fotos en wwwroot/Images se vean en Render
app.UseStaticFiles();

// Habilitamos CORS antes de los controladores
app.UseCors("AllowAll");

app.UseAuthorization();

// Mapea tus rutas de la API (/api/characters, etc.)
app.MapControllers();

app.Run();