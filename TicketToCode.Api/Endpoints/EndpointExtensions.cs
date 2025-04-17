using System.Reflection;

namespace TicketToCode.Api.Endpoints;

public static class EndpointExtensions
{
    // Metod för att registrera alla endpoints i appen baserat på en given typ (T).
    // T är ofta en endpoint-klass som används för att hitta alla endpoint-typer i samma assembly.
    public static void MapEndpoints<T>(this IApplicationBuilder app)
    {
        MapEndpoints(app, typeof(T)); // Anropar den övergripande metoden med typeof(T)
    }

    // Metod för att registrera alla endpoints i appen baserat på en viss Type.
    // Denna metod använder reflektion för att hitta alla endpoint-klasser som implementerar IEndpoint.
    public static void MapEndpoints(this IApplicationBuilder app, Type endpointReferenceType)
    {
        // Hämta alla endpoint-typer från assemblyn som innehåller den givna typen
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(endpointReferenceType);

        // För varje endpoint-typ, anropa MapEndpoint för att registrera den i appen
        foreach (var endpointType in endpointTypes)
        {
            try
            {
                // Hämtar metoden MapEndpoint från varje endpoint-klass och anropar den
                var mapEndpointMethod = endpointType.GetMethod(nameof(IEndpoint.MapEndpoint));
                if (mapEndpointMethod == null)
                {
                    // Om metoden inte finns, kasta ett fel
                    throw new InvalidOperationException($"Metoden {nameof(IEndpoint.MapEndpoint)} saknas i {endpointType.Name}.");
                }
                // Anropar MapEndpoint-metoden och registrerar den i appen
                mapEndpointMethod.Invoke(null, new object[] { app });
            }
            catch (Exception ex)
            {
                // Logga eventuella fel vid registreringen av endpointen
                Console.WriteLine($"Fel vid registrering av endpoint {endpointType.Name}: {ex.Message}");
            }
        }
    }

    // Hämta alla typer som implementerar IEndpoint från assemblyn för den givna typen
    // Den här metoden används för att hitta alla endpoint-typer som kan registreras i appen.
    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type endpointReferenceType)
    {
        // Filtrera bort abstrakta klasser och gränssnitt, och hitta alla typer som implementerar IEndpoint
        return endpointReferenceType.Assembly.DefinedTypes
            .Where(x => !x.IsAbstract && !x.IsInterface && typeof(IEndpoint).IsAssignableFrom(x));
    }
}
