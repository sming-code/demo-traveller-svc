namespace DemoApp.Services.Travellers.Api.Endpoints;
using Models;

public class CreateTravellerEndpoint : IMinimalEndpoint
{
    public void MapEndpoint(WebApplication app) =>
        app.MapPost(
            "traveller",
            async (
                [FromBody] CreateTravellerModel newTraveller,
                [FromServices] ITravellerService travellerService,
                HttpContext httpContext,
                LinkGenerator linkGenerator
            ) =>
            {
                var newTravellerIdentifier = await travellerService.QueueCreateTraveller(
                    newTraveller.FirstName,
                    newTraveller.Surname
                );

                var getTravellerLink = linkGenerator.GetUriByName(
                    httpContext,
                    "GetTravellerByIdentifier",
                    new { travellerIdentifier = newTravellerIdentifier.ToString() }
                );

                return Results.Created(
                    getTravellerLink,
                    newTravellerIdentifier
                );
            }
        )
        // .WithGroupName("Travellers")
        .WithName("CreateTraveller")
        .Produces<Guid>();
}
