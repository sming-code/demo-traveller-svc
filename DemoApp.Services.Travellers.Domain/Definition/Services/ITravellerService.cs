namespace DemoApp.Services.Travellers.Domain.Definition.Services;
using Dtos;

public interface ITravellerService
{
    Task<Guid> QueueCreateTraveller(
        string firstName,
        string surname
    );
    Task CreateTraveller(TravellerDto travellerDto);
    Task<TravellerDto[]> GetAllTravellers();
    Task<TravellerDto> GetTravellerByIdentifier(
        Guid travellerIdentifier
    );
    Task<TravellerDto[]> GetTravellersMatchingSearchCriteria(
        int? tagDesktopNumber
    );
}