using SimpsonsDle.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Servicios
builder.Services.AddSingleton<CharacterService>(); // Solo una vez es necesario

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

var app = builder.Build();

// 2. Configurar el Pipeline de HTTP
app.UseCors("AllowReactApp");

// MUY IMPORTANTE: Esto permite que el front acceda a las imágenes de wwwroot/Images
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

app.Run();