using DemoApp.Services.Travellers.Domain.Dtos;
using SmingCode.Utilities.Kafka.Producers;

namespace DemoApp.Services.Travellers.BusinessLogic;

internal class TravellerService(
    ITravellerData _travellerData,
    IKafkaProducer _kafkaProducer
) : ITravellerService
{
    public async Task<Guid> QueueCreateTraveller(
        string firstName,
        string surname
    )
    {
        var newTravellerId = Guid.NewGuid();

        await _kafkaProducer.SendEvent(
            "traveller-create",
            new TravellerDto(
                newTravellerId,
                firstName,
                surname
            )
        );

        return newTravellerId;
    }

    public async Task CreateTraveller(
        TravellerDto travellerDto
    ) => await _travellerData.CreateTraveller(
        travellerDto
    );

    public async Task<TravellerDto[]> GetAllTravellers()
        => await _travellerData.GetAllTravellers();

    public async Task<TravellerDto> GetTravellerByIdentifier(
        Guid travellerIdentifier
    ) => await _travellerData.GetTravellerByIdentifier(
        travellerIdentifier
    );

    public Task<TravellerDto[]> GetTravellersMatchingSearchCriteria(
        int? tagDesktopNumber
    )
    {
        throw new NotImplementedException();
    }
}