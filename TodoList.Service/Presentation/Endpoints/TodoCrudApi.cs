using TodoList.Service.Domain;
using TodoList.Service.Domain.Repositories;

namespace TodoList.Service.Presentation.Endpoints;

public static class TodoCrudEndpoints
{
  private static readonly string[] tags = ["Todo"];
  private const string NameIsRequiredError = "Name is required";

  public static void RegisterTodoCrudEndpoints(this WebApplication app)
  {
    app.MapGet("/todo/{id}", GetAsync).WithTags(tags);
    app.MapGet("/todo/all", GetAllAsync).WithTags(tags); 
    app.MapPost("/todo", CreateAsync).WithTags(tags);  
    app.MapPost("/todo/{id}", UpdateAsync).WithTags(tags);
    app.MapDelete("/todo/{id}", DeleteAsync).WithTags(tags);   
  }

  private static async Task<IResult> GetAsync(int id, ITodoRepository todoRepository)
  {
    var todo = await todoRepository.GetAsync(id);
    return todo is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(todo);
  }

  private static async Task<IResult> GetAllAsync(ITodoRepository todoRepository)
  {
    return Results.Ok(await todoRepository.GetAllAsync());
  }

  private static async Task<IResult> CreateAsync(Todo todo, ITodoRepository todoRepository)
  {
    if (string.IsNullOrWhiteSpace(todo.Name))
    {
      return Results.BadRequest(NameIsRequiredError);
    }

    var newTodo = await todoRepository.CreateAsync(todo);
    return Results.Created($"/todo/{newTodo.Id}", newTodo);
  }

  private static async Task<IResult> UpdateAsync(int id, Todo todo, ITodoRepository todoRepository)
  {
    if (string.IsNullOrWhiteSpace(todo.Name))
    {
      return Results.BadRequest(NameIsRequiredError);
    }

    var updated = await todoRepository.UpdateAsync(id, todo);
    return updated is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(updated); 
  }

  private static async Task<IResult> DeleteAsync(int id, ITodoRepository todoRepository)
  {
    var deleted = await todoRepository.DeleteAsync(id);
    return deleted is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(deleted);
  }
}