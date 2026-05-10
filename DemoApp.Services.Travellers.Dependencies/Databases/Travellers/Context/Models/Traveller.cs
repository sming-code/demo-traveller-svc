using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.Services.Travellers.Dependencies.Databases.Travellers.Context.Models;

public class Traveller
{
    public Guid TravellerId { get; set; }
    public required string FirstName { get; set; }
    public required string Surname { get; set; }

    public ICollection<TravellerAddress> Addresses { get; } = null!;
}

internal class TravellerEntityTypeConfiguration : IEntityTypeConfiguration<Traveller>
{
    public void Configure(EntityTypeBuilder<Traveller> builder)
    {
        builder.ToTable("Traveller");

        builder.HasKey(entity => entity.TravellerId);

        builder.Property(entity => entity.FirstName)
            .HasMaxLength(100);
        builder.Property(entity => entity.Surname)
            .HasMaxLength(100);
    }
}