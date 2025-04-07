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
            new Event { Id = 1, Title = "Konsert i Stockholm", Description = "Liveband", Location = "Stockholm", Date = DateTime.Now.AddDays(5) },
            new Event { Id = 2, Title = "Konferens i Malmö", Description = "IT och framtid", Location = "Malmö", Date = DateTime.Now.AddDays(10) },
        });
    }
}