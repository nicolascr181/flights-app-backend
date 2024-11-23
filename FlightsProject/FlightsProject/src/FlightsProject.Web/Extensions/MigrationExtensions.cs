using FlightsProject.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FlightsProject.Web.Extensions;

public static class MigrationExtensions
{
  public static void ApplyMigrations(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
  }
}
