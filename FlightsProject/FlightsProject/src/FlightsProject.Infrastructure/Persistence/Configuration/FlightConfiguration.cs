using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightsProject.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightsProject.Infrastructure.Persistence.Configuration;
public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
  public void Configure(EntityTypeBuilder<Flight> builder)
  {
    builder.Property(p => p.Origin)
        .IsRequired();

    builder.Property(p => p.Destination)
      .IsRequired();

    builder.OwnsOne(e => e.Transport, options =>
    {
      options.ToJson("TRANSPORT");
    });
  }
}
