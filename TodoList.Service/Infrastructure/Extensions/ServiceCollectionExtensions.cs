using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain.Repositories;
using TodoList.Service.Infrastructure.Contexts;
using TodoList.Service.Infrastructure.Repositories;

namespace TodoList.Service.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
  public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
  {
    services
      .AddDatabase(configuration)
      .AddServices();
  }

  private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("TodoListDbConnection");

    services.AddDbContext<TodoListContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    return services;
  }

  private static IServiceCollection AddServices(this IServiceCollection services)
  {
    services.AddTransient<ITodoRepository, TodoRepository>();
    return services;
  }
}