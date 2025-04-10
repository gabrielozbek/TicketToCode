namespace TicketToCode.Api.Endpoints;
using TicketToCode.Core.Models;

public class GetAllEvents : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events", Handle)
        .WithSummary("Get all events");

    // Request and Response types
    public record Response(
        int Id,
        string Name,
        string Description,
        string EventType,
        DateTime Start,
        DateTime End,
        int MaxAttendees
    );

    //Logic
    private static List<Response> Handle(Database db)
    {
        return db.Events
            .Select(item => new Response(
                Id: item.Id,
                Name: item.Name,
                Description: item.Description,
                EventType: item.EventType,
                Start: item.StartTime,
                End: item.EndTime,
                MaxAttendees: item.MaxAttendees
            )).ToList();
    }
}