namespace TicketToCode.Api.Endpoints;

using TicketToCode.Core.Models;
using TicketToCode.Core.Data; // för att nå Database-klassen

public class CreateEvent : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapPost("/events", Handle)
        .WithSummary("Create event");

    public record Request(
        string Name,
        string Description,
        string EventType,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    public record Response(int Id);

    private static IResult Handle(Request request, Database db) // ✅ använder nu Database direkt
    {
        int newId = db.Events.Any() ? db.Events.Max(e => e.Id) + 1 : 1;

        var ev = new Event
        {
            Id = newId,
            Name = request.Name,
            Description = request.Description,
            EventType = request.EventType,
            StartTime = request.Start,
            EndTime = request.End,
            MaxAttendees = request.MaxAttendees
        };

        db.Events.Add(ev);
        db.SaveChanges(); // funkar eftersom den är tom men finns i klassen

        return Results.Ok(new Response(ev.Id));
    }
}
