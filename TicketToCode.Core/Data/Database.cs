using TicketToCode.Core.Models;

namespace TicketToCode.Core.Data;

public interface IDatabase
{
    List<Event> Events { get; set; }
    List<User> Users { get; set; }
    List<Booking> Bookings { get; set; }
}

public class Database : IDatabase
{
    public List<Event> Events { get; set; } = new List<Event>();
    public List<User> Users { get; set; } = new List<User>();
    public List<Booking> Bookings { get; set; } = new List<Booking>();

    public Database()
    {
        Events.AddRange(new List<Event>
        {
            new Event
            {
                Id = 1,
                Name = "Konsert i Stockholm",
                Description = "Liveband på scen i centrala Stockholm",
                EventType = "Concert",
                StartTime = DateTime.Now.AddDays(5),
                EndTime = DateTime.Now.AddDays(5).AddHours(2),
                MaxAttendees = 100
            },
            new Event
            {
                Id = 2,
                Name = "Konferens i Malmö",
                Description = "Tech-konferens med gästföreläsare",
                EventType = "Conference",
                StartTime = DateTime.Now.AddDays(10),
                EndTime = DateTime.Now.AddDays(10).AddHours(6),
                MaxAttendees = 250
            }
        });
    }
}
