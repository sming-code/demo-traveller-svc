namespace DemoApp.Services.Travellers.Domain.Dependencies;
using Dtos;

public interface ITravellerData
{
    Task<Guid> CreateTraveller(
        TravellerDto travellerDto
    );
    Task<TravellerDto[]> GetAllTravellers();
    Task<TravellerDto> GetTravellerByIdentifier(
        Guid travellerIdentifier
    );
}