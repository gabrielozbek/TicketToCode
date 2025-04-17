using TicketToCode.Core.Data; // Ger tillgång till vår "fake-databas"
using TicketToCode.Core.Models; // Här finns Booking- och Event-modellerna
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace TicketToCode.Api.Endpoints;

// Klass som hanterar skapandet av en ny bokning
public class BookingCreate : IEndpoint
{
    // Registrerar endpointen för att ta emot POST-anrop till /api/bookings
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/bookings", CreateBooking) // Metodnamn ändrat för tydlighet
           .WithSummary("Create a new booking for a specific event");
    }

    // Request-modellen – det som frontend skickar in
    public record BookingRequest(int EventId, int UserId); // (tillfällig UserId tills auth finns)

    // Response-modellen – det vi skickar tillbaka till användaren
    public record BookingResponse(string Message);

    // Metod som körs när någon skickar ett POST-anrop till /api/bookings
    // Skapar en ny bokning för det specificerade eventet
    private static IResult CreateBooking(BookingRequest req, Database db)
    {
        // 1. Kolla om eventet finns
        var ev = db.Events.FirstOrDefault(e => e.Id == req.EventId);
        if (ev == null)
        {
            // Om eventet inte finns, returnera ett felmeddelande
            return Results.NotFound(new BookingResponse("Eventet finns inte."));
        }

        // 2. Räkna hur många bokningar som redan finns för eventet
        var currentBookings = db.Bookings.Count(b => b.EventId == req.EventId);
        if (currentBookings >= ev.MaxAttendees)
        {
            // Om det är fullbokat, stoppa bokningen
            return Results.BadRequest(new BookingResponse("Eventet är fullbokat."));
        }

        // 3. Kontrollera att användaren inte redan har bokat detta event
        var alreadyBooked = db.Bookings.Any(b => b.EventId == req.EventId && b.UserId == req.UserId);
        if (alreadyBooked)
        {
            // Stoppa om samma användare försöker boka igen
            return Results.BadRequest(new BookingResponse("Du har redan bokat detta event."));
        }

        // 4. Skapa och spara bokningen
        var booking = new Booking
        {
            EventId = req.EventId,
            UserId = req.UserId // OBS: I framtiden bör detta hämtas från autentisering
        };

        db.Bookings.Add(booking);      // Lägg till bokningen i databasen
        db.SaveChanges();              // Spara ändringarna

        // 5. Skicka tillbaka en lyckad respons
        return Results.Ok(new BookingResponse("Bokningen lyckades!"));
    }
}
