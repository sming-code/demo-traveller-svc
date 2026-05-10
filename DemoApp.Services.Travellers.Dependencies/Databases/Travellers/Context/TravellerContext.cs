using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers.Context;
using Models;

public class TravellerContext(
    DbContextOptions<TravellerContext> options
) : DbContext(options)
{
    public DbSet<Traveller> Travellers { get; set; }
    public DbSet<TravellerAddress> TravellerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly()
        );
}