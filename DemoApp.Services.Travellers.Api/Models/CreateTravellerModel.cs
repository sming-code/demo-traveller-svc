namespace DemoApp.Services.Travellers.Api.Models;

public class CreateTravellerModel
{
    public required int TagDesktopNumber { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }
}
