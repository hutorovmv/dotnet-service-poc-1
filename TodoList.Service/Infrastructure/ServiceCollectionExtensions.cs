using Microsoft.EntityFrameworkCore;
using TodoList.Service.Domain.Repositories;
using TodoList.Service.Infrastructure.Repositories;

namespace TodoList.Service.Infrastructure;

public static class ServiceCollectionExtensions
{
  public static void RegisterServices(this IServiceCollection services, WebApplicationBuilder builder)
  {
    var connectionString = builder.Configuration.GetConnectionString("TodoListDbConnection");
    services.AddDbContext<TodoListContext>(opt => opt.UseNpgsql(connectionString));
    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddEndpointsApiExplorer();
    services.AddOpenApiDocument(config =>
    {
      config.DocumentName = "TodoAPI";
      config.Title = "TodoAPI v1.0";
      config.Version = "v1";
    });

    services.AddTransient<ITodoRepository, TodoRepository>();
  }
}