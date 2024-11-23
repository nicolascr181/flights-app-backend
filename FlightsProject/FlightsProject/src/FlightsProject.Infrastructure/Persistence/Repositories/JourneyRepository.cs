using FlightsProject.Core.Entities;
using FlightsProject.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightsProject.Infrastructure.Persistence.Repositories;
public class JourneyRepository: IJourneyRepository
{
  private readonly ApplicationDbContext _context;

  public JourneyRepository(ApplicationDbContext context)
  {
    _context = context ?? throw new ArgumentNullException(nameof(context));
  }
  public async Task<List<Journey>> GetJourneysAsync() => await _context.Journeys.ToListAsync(); 
  
}
