using Microsoft.EntityFrameworkCore.Design;

namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers;
using Context;
using Microsoft.EntityFrameworkCore;

public class TravellerContextFactory : IDesignTimeDbContextFactory<TravellerContext>
{
    public TravellerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TravellerContext>()
            .UseSqlServer("Data Source=/home/matt/data/traveller.db");

        return new TravellerContext(optionsBuilder.Options);
    }
}