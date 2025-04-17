using Microsoft.AspNetCore.Components.Web;
using TicketToCode.Client;
using TicketToCode.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Registrera Razor-komponenter med WebAssembly-stöd
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Registrera HttpClient med cookies aktiverade
builder.Services.AddScoped(sp => new HttpClient(new HttpClientHandler
{
    UseCookies = true // Aktivera cookies för autentisering
})
{
    BaseAddress = new Uri("https://localhost:7206/") // Din API-adress
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
else
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery(); // Viktigt för säkerheten

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode();

app.Run();
