namespace TicketToCode.Api.Endpoints;
using TicketToCode.Core.Models;

public class GetEvent : IEndpoint
{
    // Mapping för GET-anrop
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events/{id}", Handle)
        .WithSummary("Get event by ID");

    // Request och Response-typer
    public record Request(int Id);

    public record Response(
        int Id,
        string Name,
        string Description,
        string EventType,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    // Logik för att hämta event
    private static IResult Handle([AsParameters] Request request, Database db)
    {
        // Validering av request
        if (request.Id <= 0)
        {
            return Results.BadRequest(new { Message = "Event ID must be greater than zero." });
        }

        // Hitta eventet i databasen
        var ev = db.Events.FirstOrDefault(e => e.Id == request.Id);

        // Om eventet inte hittas, returnera 404
        if (ev == null)
        {
            return Results.NotFound(new { Message = "Event not found." });
        }

        // Mappar Event till Response DTO
        var response = new Response(
            Id: ev.Id,
            Name: ev.Name,
            Description: ev.Description,
            EventType: ev.EventType,
            Start: ev.StartTime,
            End: ev.EndTime,
            MaxAttendees: ev.MaxAttendees
        );

        // Returnera response med 200 OK
        return Results.Ok(response);
    }
}
