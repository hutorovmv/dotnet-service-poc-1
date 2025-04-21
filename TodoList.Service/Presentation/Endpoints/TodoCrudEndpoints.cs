using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Service.Utils.Models;
using TodoList.Service.Domain.Entities;
using TodoList.Service.Domain.Repositories;
using TodoList.Service.Infrastructure.Extensions;

namespace TodoList.Service.Presentation.Endpoints;

public static class TodoCrudEndpoints
{
  private static readonly string[] tags = ["Todo"];
  private const string NameIsRequiredError = "Name is required";
  private const string Prefix = "api/todo";

  public static void RegisterTodoCrudEndpoints(this WebApplication app)
  {
    app.MapGet($"{Prefix}/{{id}}", GetAsync).WithTags(tags);
    app.MapGet($"{Prefix}/all", GetAllAsync).WithTags(tags);
    app.MapPost($"{Prefix}", CreateAsync).WithTags(tags);
    app.MapPost($"{Prefix}/{{id}}", UpdateAsync).WithTags(tags);
    app.MapDelete($"{Prefix}/{{id}}", DeleteAsync).WithTags(tags);
  }

  [Authorize]
  private static async Task<IResult> GetAsync(
    int id,
    ITodoRepository todoRepository,
    ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();

    var todo = await todoRepository.GetAsync(id, user.Id);

    return todo is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(new ApiResponse<Todo>(true, todo));
  }

  [Authorize]
  private static async Task<IResult> GetAllAsync(ITodoRepository todoRepository, ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();
    var todos = await todoRepository.GetAllAsync(user.Id);
    return Results.Ok(new ApiResponse<IEnumerable<Todo>>(true, todos));
  }

  [Authorize]
  private static async Task<IResult> CreateAsync(
    Todo todo,
    ITodoRepository todoRepository,
    ClaimsPrincipal principal)
  {
    if (string.IsNullOrWhiteSpace(todo.Name))
    {
      return Results.BadRequest(NameIsRequiredError);
    }

    var user = principal.ToUserEntity();

    var newTodo = await todoRepository.CreateAsync(todo, user.Id);
    return Results.Created($"api/todo/{newTodo.Id}", new ApiResponse<Todo>(true, newTodo));
  }

  [Authorize]
  private static async Task<IResult> UpdateAsync(
    int id,
    Todo todo,
    ITodoRepository todoRepository,
    ClaimsPrincipal principal)
  {
    if (string.IsNullOrWhiteSpace(todo.Name))
    {
      return Results.BadRequest(new ApiResponse<object>(false, todo, NameIsRequiredError));
    }

    var user = principal.ToUserEntity();

    var updated = await todoRepository.UpdateAsync(id, todo, user.Id);
    return updated is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(new ApiResponse<Todo>(true, updated));
  }

  [Authorize]
  private static async Task<IResult> DeleteAsync(
    int id,
    ITodoRepository todoRepository,
    ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();

    var deleted = await todoRepository.DeleteAsync(id, user.Id);

    return deleted is { IsNull: true }
      ? Results.NotFound()
      : Results.Ok(new ApiResponse<Todo>(true, deleted));
  }
}