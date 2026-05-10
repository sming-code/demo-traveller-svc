namespace DemoApp.Services.Travellers.Domain.Dtos;

public record TravellerDto(
    Guid TravellerIdentifier,
    string FirstName,
    string Surname
);