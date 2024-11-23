using Ardalis.SharedKernel;
using FlightsProject.Core.Entities;
using FlightsProject.Core.Primitives;
using FlightsProject.UseCases.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FlightsProject.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
  private readonly IPublisher? _publisher;

  public ApplicationDbContext(DbContextOptions options, IPublisher? publisher) : base(options)
  {
    _publisher = publisher;
  }

  public DbSet<Journey> Journeys { get ; set ; }
  public DbSet<Flight> Flights { get ; set ; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
  }

  /// <summary>
  /// Capture all events domain
  /// </summary>
  /// <param name="cancellationToken"></param>
  /// <returns></returns>
  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var domainEvents = ChangeTracker.Entries<AggregateRoot>()
        .Select(e => e.Entity)
        .Where(e => e.GetDomainEvents().Any())
        .SelectMany(e => e.GetDomainEvents());

    var result = await base.SaveChangesAsync(cancellationToken);

    return result;
  }
}
