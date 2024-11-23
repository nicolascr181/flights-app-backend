using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightsProject.Infrastructure.Persistence.Repositories;
public class FlightRepository : IFlightRepository
{
  private readonly ApplicationDbContext _context;

  public FlightRepository(ApplicationDbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }
  public async Task<List<Flight>> GetFlightsAsync() => await _context.Flights.ToListAsync();
}
