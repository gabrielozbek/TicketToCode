namespace TicketToCode.Core.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string EventType { get; set; } = "";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int MaxAttendees { get; set; }
}
public enum EventType
{
    Concert = 0,
    Festival,
    Theatre,
    Other
}