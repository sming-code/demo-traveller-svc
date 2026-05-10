using Microsoft.Extensions.Configuration;

namespace DemoApp.Services.Travellers.BusinessLogic;
using Dependencies;
using SmingCode.Utilities.Kafka;

public static class DependencyInjection
{
    public static IServiceCollection InitialiseBusinessLogic(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ITravellerService, TravellerService>();
        services.InitialiseDependencies(configuration);

        return services;
    }
}