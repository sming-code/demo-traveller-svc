namespace DemoApp.Services.Travellers.Api.Models;

public record TravellerModel(
    Guid TravellerIdentifier,
    string FirstName,
    string Surname
);

internal static class TravellerModelExtensions
{
    internal static TravellerModel ToModel(
        this TravellerDto travellerDto
    ) => new(
        travellerDto.TravellerIdentifier,
        travellerDto.FirstName,
        travellerDto.Surname
    );
}