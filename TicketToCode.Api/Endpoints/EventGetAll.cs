using TicketToCode.Core.Data;
using TicketToCode.Core.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using TicketToCode.Api.Endpoints;

namespace TicketToCode.Api.Endpoints;

public class EventGetAll : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/events", (Database db) =>
        {
            return Results.Ok(db.Events);
        });
    }
}
