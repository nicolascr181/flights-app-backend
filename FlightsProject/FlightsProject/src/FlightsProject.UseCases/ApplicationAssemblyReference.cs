using System.Reflection;


namespace FlightsProject.UseCases;
  public class ApplicationAssemblyReference
  {
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
  }
