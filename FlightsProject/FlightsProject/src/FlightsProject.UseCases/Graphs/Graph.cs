using FlightsProject.Core.Entities;
namespace FlightsProject.UseCases.Graphs;
public static class Graph
{



  /// <summary>
  /// Function to find all paths from a source to a destination in a graph
  /// </summary>
  /// <param name="flights"> list all of flights</param>
  /// <param name="source"> origin of the journey</param>
  /// <param name="destination"> destination of the journey</param>
  /// <returns></returns>
  public static List<List<Flight>> FindAllPaths(List<Flight> flights, string source, string destination)
  {
    var allPaths = new List<List<Flight>>();
    var currentPath = new List<Flight>();
    var visited = new HashSet<string>();

    DFS(flights, source, destination, currentPath, allPaths, visited, null);

    return allPaths;
  }


  /// <summary>
  /// Depth-first search (DFS) function to explore paths in the graph
  /// </summary>
  /// <param name="flights"> All available flights</param>
  /// <param name="current"> current origin</param>
  /// <param name="destination"> current destination</param>
  /// <param name="currentPath"> list of flights for a single journey</param>
  /// <param name="allPaths"> list of flights for every journey</param>
  /// <param name="visited">list of visited nodes in the graph </param>
  /// <param name="flightJourney"> the flight which will be part of the journey flights</param>
  private static void DFS(List<Flight> flights, string current, string destination, List<Flight> currentPath, List<List<Flight>> allPaths, HashSet<string> visited, Flight? flightJourney)
  {
    visited.Add(current);
    if (flightJourney is not null) currentPath.Add(flightJourney);

    if (current == destination)
    {

      allPaths.Add(new List<Flight>(currentPath));
    }
    else
    {
      List<Flight> filterFlights = flights.FindAll(f => f.Origin == current);
      foreach (var flight in filterFlights)
      {
        if (!String.IsNullOrEmpty(flight.Destination) && !visited.Contains(flight.Destination))
        {
          DFS(flights.FindAll(f => f.Origin != current), flight.Destination, destination, currentPath, allPaths, visited, flight);
        }
      }
    }

    if (flightJourney is not null) currentPath.RemoveAt(currentPath.Count - 1);
    visited.Remove(current);
  }
}
