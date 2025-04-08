using Microsoft.AspNetCore.Components.Web;
using TicketToCode.Client;
using TicketToCode.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Lägg till services för både server och WebAssembly
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Lägg till HttpClient för WebAssembly
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7206/") // ← ändra om ditt API kör på annan port
});

var app = builder.Build();

// Konfigurera pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging(); // ← viktigt för WebAssembly
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();