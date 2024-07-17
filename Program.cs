using Esercitazione.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddScoped<IClientiService, ClientiService>()
    .AddControllersWithViews();

// CONFIGURAZIONE DELL'AUTENTICAZIONE
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        // Pagina alla quale l'utente sara indirizzato se non � stato gi� riconosciuto
        opt.LoginPath = "/Account/Login";
    });
// Fine configurazione

// CONFIGURAZIONE SERVIZIO DI GESTIONE DELLE AUTENTICAZIONI
builder.Services
    .AddScoped<IClientiService, ClientiService>()
    .AddScoped<IAuthSvc, AuthSvc>();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
