namespace DemoApp.Services.Travellers.Api.Endpoints;
using Models;

public class GetAllTravellersEndpoint : IMinimalEndpoint
{
    public void MapEndpoint(WebApplication app) =>
        app.MapGet(
            "traveller",
            async (
                [FromServices] ITravellerService travellerService
            ) =>
            {
                var allTravellers = await travellerService.GetAllTravellers();

                var response = allTravellers
                    .Select(traveller => traveller.ToModel());

                return Results.Ok(
                    response
                );
            }
        )
        .WithGroupName("Travellers")
        .WithName("GetAllTravellers")
        .Produces<TravellerModel[]>();
}
