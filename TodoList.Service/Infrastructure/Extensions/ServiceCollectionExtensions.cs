using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.Generation.Processors.Security;
using TodoList.Service.Domain.Repositories;
using TodoList.Service.Infrastructure.Contexts;
using TodoList.Service.Infrastructure.Models.Options;
using TodoList.Service.Infrastructure.Repositories;

namespace TodoList.Service.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    services
      .AddDatabase(builder)
      .AddAuth(builder)
      .AddSwagger()
      .AddServices()
      .ConfigureCors();
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services, WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("TodoListDbConnection");

    services.AddDbContext<TodoListContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    return services;
  }

  private static IServiceCollection AddAuth(this IServiceCollection services, WebApplicationBuilder builder)
  {
    var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? new JwtOptions();

    services
      .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = true,
          ValidIssuer = jwtOptions.Issuer,
          ValidateAudience = true,
          ValidAudience = jwtOptions.Audience,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret))
        };
      });
    services.AddAuthorization();

    return services;
  }

  private static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddOpenApiDocument(config =>
    {
      config.DocumentName = "TodoAPI";
      config.Title = "TodoAPI v1.0";
      config.Version = "v1";

      config.AddSecurity("JWT", new OpenApiSecurityScheme
      {
        Type = OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Enter the token"
      });
      config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
    });

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ITodoRepository, TodoRepository>();
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