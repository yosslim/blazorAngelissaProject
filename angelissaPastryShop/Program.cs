using angelissaPastryShop.Components;
using angelissaPastryShop.Data;
using angelissaPastryShop.Repository;
using angelissaPastryShop.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

//database configuration: we need:
/*
Microsoft.EntityFrameworkCore.Tools: para migraciones
Microsoft.EntityFrameworkCore.SqlServe: para trabajar con sql server para DB
Microsoft.EntityFrameworkCore: para entity
*/

builder.Services.AddDbContext<AngelissaPastryContext>(
    options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("ConnectionDBFromAzure")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//builder.Services.AddServerSideBlazor();

//Repository as DI

builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IOrdenRepositorio, OrdenRepositorio>();
builder.Services.AddScoped<CarritoServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//app.MapBlazorHub();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
