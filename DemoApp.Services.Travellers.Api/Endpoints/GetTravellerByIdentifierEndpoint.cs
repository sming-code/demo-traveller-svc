namespace DemoApp.Services.Travellers.Api.Endpoints;
using Models;

public class GetTravellerByIdentifierEndpoint : IMinimalEndpoint
{
    public void MapEndpoint(WebApplication app) =>
        app.MapGet(
            "traveller/{travellerIdentifier}",
            async (
                Guid travellerIdentifier,
                [FromServices] ITravellerService travellerService
            ) =>
            {
                var travellerDto = await travellerService.GetTravellerByIdentifier(
                    travellerIdentifier
                );

                var travellerModel = travellerDto.ToModel();

                return Results.Ok(
                    travellerModel
                );
            }
        )
        .WithGroupName("Travellers")
        .WithName("GetTravellerByIdentifier")
        .Produces<TravellerModel>();
}
