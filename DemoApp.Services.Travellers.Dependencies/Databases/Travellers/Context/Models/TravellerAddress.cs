using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers.Context.Models;

public class TravellerAddress
{
    public Guid TravellerAddressId { get; set; }
    public required Guid TravellerId { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string TownCity { get; set; }
    public required string County { get; set; }
    public required string PostCode { get; set; }
    public required string Country { get; set; }

    public Traveller Traveller { get; set; } = null!;
}

internal class TravellerAddressEntityTypeConfiguration : IEntityTypeConfiguration<TravellerAddress>
{
    public void Configure(EntityTypeBuilder<TravellerAddress> builder)
    {
        builder.ToTable("TravellerAddress");

        builder.HasKey(entity => entity.TravellerAddressId);

        builder.HasOne(entity => entity.Traveller)
            .WithMany(foreign => foreign.Addresses)
            .HasForeignKey(entity => entity.TravellerId);

        builder.Property(entity => entity.AddressLine1)
            .HasMaxLength(100);
        builder.Property(entity => entity.AddressLine2)
            .HasMaxLength(100);
        builder.Property(entity => entity.TownCity)
            .HasMaxLength(100);
        builder.Property(entity => entity.County)
            .HasMaxLength(100);
        builder.Property(entity => entity.PostCode)
            .HasMaxLength(15);
        builder.Property(entity => entity.Country)
            .HasMaxLength(100);
    }
}