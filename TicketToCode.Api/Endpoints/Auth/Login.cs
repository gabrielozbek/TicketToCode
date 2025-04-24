using TicketToCode.Api.Services;

namespace TicketToCode.Api.Endpoints.Auth;

public class Login : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/auth/login", Handle)
        .WithSummary("Logga in med användarnamn och lösenord")
        .AllowAnonymous();

    // Datamodeller för begäran och svar
    public record Request(string Username, string Password);
    public record Response(string Username, string Role);

    // Huvudlogiken för inloggning
    private static Results<Ok<Response>, NotFound<string>> Handle(
        Request request,
        IAuthService authService,
        HttpContext context)
    {
        var result = authService.Login(request.Username, request.Password);
        if (result == null)
        {
            return TypedResults.NotFound("Ogiltigt användarnamn eller lösenord");
        }

        //  Skapa en enkel autentiseringscookie
        context.Response.Cookies.Append("auth", $"{result.Username}:{result.Role}", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTimeOffset.UtcNow.AddDays(7)
        });
        var response = new Response(result.Username, result.Role); //skapa svar
        return TypedResults.Ok(response); // skicka tillbaka svaret
    }
}
