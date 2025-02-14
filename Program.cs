using Radzen;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IDEA_Holding_Test.Services;

var builder = WebApplication.CreateBuilder(args);

// Dodata konekcija sa bazom podataka
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Dodat ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()  // Dodata podrška za uloge
    .AddEntityFrameworkStores<ApplicationDbContext>();


// Dodat AuthenticationStateProvider za Blazor
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

// Dodat UserManager i SignInManager
builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();

// Dodat Blazor Server servise
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Konfiguracija middleware-a
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();  // Omogućena autentifikaciju
app.UseAuthorization();   // Omogućena autorizaciju

// Mapiranje Blazor i API ruta
app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToFile("index.html");

app.Run();
