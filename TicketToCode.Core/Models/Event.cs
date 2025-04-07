namespace TicketToCode.Core.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
}
public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Other
}