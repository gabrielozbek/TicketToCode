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
    BaseAddress = new Uri("https://localhost:7206/") // ← din API-url, korrekt!
});

var app = builder.Build();

// Felhantering och säkerhet
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 🧩 Viktig ordning börjar här
app.UseRouting();
app.UseAntiforgery(); // ✅ MÅSTE komma efter UseRouting, men före MapRazorComponents

// Mappar till Blazor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
