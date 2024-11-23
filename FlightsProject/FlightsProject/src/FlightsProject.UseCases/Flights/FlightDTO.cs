using FlightsProject.Core.Entities;

namespace FlightsProject.UseCases.Flights;
public record FlightDTO(string? origin, string? destination, double? price, Transport? transport);
