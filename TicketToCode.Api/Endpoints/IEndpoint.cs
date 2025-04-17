namespace TicketToCode.Api.Endpoints;

/// <summary>
/// Gränssnitt som definierar en metod för att mappa en endpoint i applikationen.
/// Alla endpoint-klasser som implementerar detta gränssnitt måste tillhandahålla
/// en implementation av metoden <see cref="MapEndpoint"/> för att registrera
/// sin specifika rutt i applikationen.
/// </summary>
public interface IEndpoint
{
    /// <summary>
    /// Statisk metod som alla endpoint-klasser måste implementera för att registrera sin endpoint i applikationen.
    /// Metoden anropas under applikationens start för att mappa endpoints till specifika HTTP-rutter.
    /// </summary>
    /// <param name="app">En instans av <see cref="IEndpointRouteBuilder"/> som används för att definiera rutter i applikationen.</param>
    public static abstract void MapEndpoint(IEndpointRouteBuilder app);
}
