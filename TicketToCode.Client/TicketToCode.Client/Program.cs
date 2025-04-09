using Microsoft.AspNetCore.Components.Web;
using TicketToCode.Client;
using TicketToCode.Client.Components;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents(); 


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
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery(); // viktigt!

// Endast WebAssembly-rendering
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode(); 

app.Run();