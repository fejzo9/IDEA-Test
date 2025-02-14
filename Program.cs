using Radzen;
using IDEA_Holding_Test.Components;

var builder = WebApplication.CreateBuilder(args);

// Dodao Radzen servise
builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

// Ostale postojeće konfiguracije
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToFile("index.html");

app.Run();
