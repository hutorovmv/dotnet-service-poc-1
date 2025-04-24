using Identity.Service.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Service.Utils.Middlewares;

namespace Identity.Service.Presentation.Extensions;

public static class WebApplicationExtensions
{
  public static void ConfigureWebApplication(this WebApplication app)
  {
    app
      .ConfigureSwagger()
      .ConfigureDatabase()
      .ConfigureHttps()
      .ConfigureMiddleware();
  }

  private static WebApplication ConfigureSwagger(this WebApplication app)
  {
    if (app.Environment.IsDevelopment())
    {
      app.UseOpenApi();
      app.UseSwaggerUi(config =>
      {
        config.DocumentTitle = "IdentityAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
      });
    }

    return app;
  }

  private static WebApplication ConfigureDatabase(this WebApplication app)
  {
    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<IdentityContext>();
      db.Database.Migrate();
    }

    return app;
  }

  private static WebApplication ConfigureHttps(this WebApplication app)
  {
    app.UseHttpsRedirection();
    return app;
  }

  private static WebApplication ConfigureMiddleware(this WebApplication app)
  {
    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseMiddleware<ExceptionsMiddleware>();
    return app;
  }
}