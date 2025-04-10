using TicketToCode.Core.Data;
using TicketToCode.Core.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using TicketToCode.Api.Endpoints;

namespace TicketToCode.Api.Endpoints;

public class BookingCreate : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings", (BookingRequest req, Database db) =>
        {
            var booking = new Booking
            {
                EventId = req.EventId,
                UserId = 1, // TODO: byt till riktig auth senare
            };

            db.Bookings.Add(booking);
            return Results.Ok(booking);
        });
    }

    public record BookingRequest(int EventId);
}


