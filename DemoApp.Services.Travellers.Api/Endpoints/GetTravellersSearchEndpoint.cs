namespace DemoApp.Services.Travellers.Api.Endpoints;
using Models;

public class GetTravellersSearchEndpoint : IMinimalEndpoint
{
    public void MapEndpoint(WebApplication app) =>
        app.MapGet(
            "traveller",
            async (
                int? tagDesktopNumber,
                [FromServices] ITravellerService travellerService
            ) =>
            {
                var results = await travellerService.GetTravellersMatchingSearchCriteria(
                    tagDesktopNumber
                );

                var travellerModel = results
                    .Select(result => result.ToModel());

                return Results.Ok(
                    travellerModel
                );
            }
        )
        .WithGroupName("Travellers")
        .WithName("GetTravellersSearch")
        .Produces<TravellerModel[]>();
}
