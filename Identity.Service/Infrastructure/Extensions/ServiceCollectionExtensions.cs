using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Identity.Service.Infrastructure.Services;
using Identity.Service.Infrastructure.Contexts;
using Identity.Service.Infrastructure.Models.Options;

namespace Identity.Service.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services
      .AddDatabase(builder)
      .AddAuth(builder)
      .AddSwagger()
      .AddServices()
      .AddCors();
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("TodoListDbConnection");

    services.AddDbContext<IdentityContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    return services;
  }

  private static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
    services.AddIdentity<IdentityUser, IdentityRole>()
      .AddEntityFrameworkStores<IdentityContext>()
      .AddDefaultTokenProviders();

    return services;
  }

  private static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddOpenApiDocument(config =>
    {
      config.DocumentName = "IdentityAPI";
      config.Title = "IdentityAPI v1.0";
      config.Version = "v1";
    });

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ITokenService, TokenService>();
    return services;
  }

  private static IServiceCollection ConfigureCors(this IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy(
        "AllowDevelopment",
        b => b.WithOrigins("https://localhost:*/*", "http://localhost:*/*")
              .AllowAnyHeader()
              .AllowAnyMethod());
    });
    return services;
  }
}