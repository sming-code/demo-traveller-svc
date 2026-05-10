namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers.Mappers;
using Context.Models;

internal static class TravellerMapper
{
    internal static TravellerDto ToDto(
        this Traveller entity
    ) => new(
        entity.TravellerId,
        entity.FirstName,
        entity.Surname
    );
}