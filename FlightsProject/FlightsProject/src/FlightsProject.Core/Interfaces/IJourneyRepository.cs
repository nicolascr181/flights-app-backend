using FlightsProject.Core.Entities;

namespace FlightsProject.Core.Interfaces;
public interface IJourneyRepository
{
  Task<List<Journey>> GetJourneysAsync();
}
