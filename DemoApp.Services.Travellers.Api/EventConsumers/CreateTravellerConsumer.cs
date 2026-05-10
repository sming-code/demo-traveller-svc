using System.Text.Json;

namespace DemoApp.Services.Travellers.Api.EventConsumers;

public class CreateTravellerConsumer : IMinimalConsumer
{
    public void Consume(IServiceCollection services) =>
        services.MapConsumer(
            "traveller-create",
            async (
                [FromEventValue] TravellerDto travellerDto,
                ITravellerService travellerService,
                ILogger<CreateTravellerConsumer> logger
            ) =>
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation(
                        "Received message on traveller-create topic, with value '{EventValue}'",
                        JsonSerializer.Serialize(travellerDto)
                    );                    
                }

                await travellerService.CreateTraveller(
                    travellerDto
                );

                logger.LogInformation(
                    "Traveller created with id {Traveller Id}",
                    travellerDto.TravellerIdentifier
                );

                return KafkaEventResult.Complete;
            }
        ).CreateTopicIfNotExists();
}