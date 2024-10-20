using API_Peliculas.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agregar el servicio de sesión
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Duración de la sesión
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Necesario para mantener la sesión en GDPR
});

// Agregar la autenticación por cookies
builder.Services.AddAuthentication("CookieAuth")
    .AddCookie("CookieAuth", config =>
    {
        config.Cookie.Name = "UserLoginCookie"; // Nombre de la cookie
        config.LoginPath = "/UsuariosControlador/Login"; // Ruta para iniciar sesión
        config.AccessDeniedPath = "/UsuariosControlador/AccessDenied"; // Ruta de acceso denegado
    });

// Add services to the container.
builder.Services.AddDbContext<APIContext>(opciones => opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirAngular");

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
