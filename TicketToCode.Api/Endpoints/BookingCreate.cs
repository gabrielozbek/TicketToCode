using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using TicketToCode.Core.Data;
using TicketToCode.Core.Models;

namespace TicketToCode.Api.Endpoints.Booking;

public static class Create
{
    public static IEndpointRouteBuilder MapBookingCreate(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/bookings", async (
            BookingRequest req,
            Database db,
            HttpContext context) =>
        {
            
            var userId = 1; 

            var booking = new Booking
            {
                EventId = req.EventId,
                UserId = userId
            };

            db.Bookings.Add(booking);
            await db.SaveChangesAsync();
            return Results.Ok(booking);
        });

        return endpoints;
    }

    public record BookingRequest(int EventId);
}