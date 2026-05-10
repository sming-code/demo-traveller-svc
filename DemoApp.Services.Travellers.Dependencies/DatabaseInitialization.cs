using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmingCode.Utilities.StartupProcesses;

namespace DemoApp.Services.Travellers.Dependencies;
using Databases.Travellers.Context;

internal class DatabaseInitialization : IServiceInitializer
{
    public Delegate ServiceInitializer =>
        async (
            TravellerContext travellerContext,
            ILogger<DatabaseInitialization> logger
        ) =>
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(
                    "Got inside database initialization. db connection string is {connString}",
                    travellerContext.Database.GetConnectionString()
                );
            }

            var pendingMigrations = await travellerContext.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                travellerContext.Database.Migrate();
            }
        };
}