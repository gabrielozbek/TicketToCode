﻿namespace TicketToCode.Api.Endpoints;
using TicketToCode.Core.Models;

public class GetEvent : IEndpoint
{
    // Mapping
    public static void MapEndpoint(IEndpointRouteBuilder app) => app
        .MapGet("/events/{id}", Handle)
        .WithSummary("Get event");

    // Request and Response types
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


    //Logic
    private static Response Handle([AsParameters] Request request, Database db)
    {
        var ev = db.Events.Find(ev => ev.Id == request.Id);

        // map ev to response dto
        var response = new Response(
            Id: ev.Id,
            Name: ev.Name,
            Description: ev.Description,
            EventType: ev.EventType,
            Start: ev.StartTime,
            End: ev.EndTime,
            MaxAttendees: ev.MaxAttendees
            );

        return response;
    }
}