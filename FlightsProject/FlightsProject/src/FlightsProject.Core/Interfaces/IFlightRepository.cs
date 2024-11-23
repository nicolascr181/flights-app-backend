using FlightsProject.Core.Entities;

namespace FlightsProject.Core.Interfaces;
public interface IFlightRepository
{
  Task<List<Flight>> GetFlightsAsync();
}
