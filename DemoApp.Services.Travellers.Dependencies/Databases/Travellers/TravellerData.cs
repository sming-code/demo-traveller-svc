using Microsoft.EntityFrameworkCore;

namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers;
using Context;
using Context.Models;
using Mappers;

internal class TravellerData(
    TravellerContext _travellerContext
) : ITravellerData
{
    public async Task<Guid> CreateTraveller(
        TravellerDto travellerDto
    )
    {
        var newEntity = new Traveller
        {
            TravellerId = travellerDto.TravellerIdentifier,
            FirstName = travellerDto.FirstName,
            Surname = travellerDto.Surname
        };

        _travellerContext.Add(newEntity);

        await _travellerContext.SaveChangesAsync();
        return newEntity.TravellerId;
    }

    public async Task<TravellerDto[]> GetAllTravellers()
        => await _travellerContext.Travellers
            .AsNoTracking()
            .Select(entity => entity.ToDto())
            .ToArrayAsync();

    public async Task<TravellerDto> GetTravellerByIdentifier(
        Guid travellerIdentifier
    )
    {
        var travellerEntity = await _travellerContext
            .Travellers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                entity => entity.TravellerId == travellerIdentifier
            )
            ?? throw new Exception("Not good");

        return travellerEntity.ToDto();
    }
}