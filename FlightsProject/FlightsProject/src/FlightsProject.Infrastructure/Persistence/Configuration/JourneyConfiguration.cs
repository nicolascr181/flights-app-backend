using FlightsProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightsProject.Infrastructure.Persistence.Configuration;
public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
{
  public void Configure(EntityTypeBuilder<Journey> builder)
  {
    builder.Property(p => p.Origin)
        .IsRequired();

    builder.Property(x => x.Destination)
      .IsRequired();
  }
}
