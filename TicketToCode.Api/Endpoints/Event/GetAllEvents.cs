namespace TicketToCode.Api.Endpoints;
using TicketToCode.Core.Models;

public class GetAllEvents : IEndpoint
{
    // Mapping för GET-anrop
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events", Handle)
        .WithSummary("Get all events");

    // Response-typ
    public record Response(
        int Id,
        string Name,
        string Description,
        string EventType,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    // Logik för att hämta alla event
    private static IResult Handle(Database db)
    {
        var events = db.Events
            .Select(item => new Response(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                EventType: item.EventType,
                Start: item.StartTime,
                End: item.EndTime,
                MaxAttendees: item.MaxAttendees
            )).ToList();

        // Om inga events finns, returnera en tom lista med 200 OK
        if (!events.Any())
        {
            return Results.Ok(new { Message = "No events found." });
        }

        // Returnera alla events med 200 OK
        return Results.Ok(events);
    }
}
