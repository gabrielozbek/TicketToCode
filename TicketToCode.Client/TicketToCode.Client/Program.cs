using Microsoft.AspNetCore.Components.Web;
using TicketToCode.Client;
using TicketToCode.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Registrera Razor-komponenter med WebAssembly-stöd
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Registrera HttpClient för API-anrop
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7206/")
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging(); // För interaktivitet via WebAssembly
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery(); // viktigt!

// Aktivera komponentrendering
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();