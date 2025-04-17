using TodoList.Service.Domain;
using TodoList.Service.Domain.Repositories;

namespace TodoList.Service.Presentation.Endpoints;

public static class TodoCrudEndpoints
{
  public static void RegisterTodoCrudEndpoints(this WebApplication app)
  {
    app.MapGet("/todo/{id}", async (int id, ITodoRepository todoRepository) =>
    {
      var todo = await todoRepository.GetAsync(id);
      return todo is { IsNull: true }
        ? Results.NotFound()
        : Results.Ok(todo);
    });

    app.MapGet("/todo/all", async (ITodoRepository todoRepository) => await todoRepository.GetAllAsync()); 
    
    app.MapPost("/todo", async (Todo todo, ITodoRepository todoRepository) => await todoRepository.CreateAsync(todo));  
    
    app.MapDelete("/todo/{id}", async (int id, ITodoRepository todoRepository) =>
    {
      var deleted = await todoRepository.DeleteAsync(id);
      return deleted is { IsNull: true }
        ? Results.NotFound()
        : Results.Ok();
    });
  }
}