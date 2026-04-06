using SistemaSuscripciones.BusinessLogic;
using SistemaSuscripciones.Data;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// ZONA DE CONFIGURACIÃ“N DE SERVICIOS (ANTES DEL BUILD)
builder.Services.AddControllersWithViews();

// ConfiguraciÃ³n del esquema de AutenticaciÃ³n por Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Redirige aquÃ­ si intenta entrar sin loguearse
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // La sesiÃ³n caduca en 30 min de inactividad
        options.AccessDeniedPath = "/Home/Error";
    });

// AquÃ­ va toda tu InyecciÃ³n de Dependencias
SistemaSuscripciones.Data.InyectorDependencias.RegistrarRepositorios(builder.Services);
builder.Services.AddScoped<ISuscripcionService, SuscripcionService>();

// Registrar el nuevo servicio de Usuario
builder.Services.AddScoped<IUsuarioService, UsuarioService>();


// CONSTRUCCIÃ“N DE LA APLICACIÃ“N
var app = builder.Build();


// PIPELINE DE PETICIONES HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Suscripcion}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();