using Application.Common.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace FlightsProject.UseCases;
public static class DependencyInjection
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(config => {
      config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyReference>();
    });

    services.AddScoped(
        typeof(IPipelineBehavior<,>),
        typeof(ValidationBehavior<,>)
    );

    services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyReference>();


    return services;
  }
}
