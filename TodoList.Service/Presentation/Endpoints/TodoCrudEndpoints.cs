using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Identity.Service.Presentation.Models;
using Identity.Service.Domain.Entities;
using Identity.Service.Domain.Repositories;
using Identity.Service.Infrastructure.Extensions;

namespace Identity.Service.Presentation.Endpoints;

public static class TodoCrudEndpoints
{
  private static readonly string[] tags = ["Todo"];
  private const string NameIsRequiredError = "Name is required";
  private const string Prefix = "api/todo";

  public static void RegisterTodoCrudEndpoints(this WebApplication app)
  {
    app.MapGet($"{Prefix}/{{id}}", GetAsync)
      .WithTags(tags)
      .CacheOutput(p =>
        p.Expire(TimeSpan.FromSeconds(ApiUtilConstants.CacheTTL.Warm))
         .SetVaryByQuery("id")
         .SetVaryByHeader(ApiUtilConstants.Headers.Authorization)
         .Tag("Todo"))
      .WithMetadata(new ResponseCacheAttribute
      {
        VaryByQueryKeys = ["id"],
        VaryByHeader = ApiUtilConstants.Headers.Authorization,
        Duration = ApiUtilConstants.CacheTTL.Warm
      });
    app.MapGet($"{Prefix}/all", GetAllAsync)
      .WithTags(tags)
      .CacheOutput(p =>
        p.Expire(TimeSpan.FromSeconds(ApiUtilConstants.CacheTTL.Warm))
         .SetVaryByHeader(ApiUtilConstants.Headers.Authorization)
         .Tag("Todo_All"))
      .WithMetadata(new ResponseCacheAttribute
      {
        VaryByHeader = ApiUtilConstants.Headers.Authorization,
        Duration = ApiUtilConstants.CacheTTL.Warm
      });

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
    Log.Logger.Debug("Getting todos for the user: {UserName}.", user.UserName);

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
      Log.Logger.Debug("Todo creation has failed. Name should not be null or empty.\n{Todo}", JsonSerializer.Serialize(todo));
      return Results.BadRequest(NameIsRequiredError);
    }

    var user = principal.ToUserEntity();
    var newTodo = await todoRepository.CreateAsync(todo, user.Id);

    Log.Logger.Debug("Todo has been created for user: {UserName}.\n{Todo}", user.UserName, JsonSerializer.Serialize(todo));
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
      Log.Logger.Debug("Todo update has failed. Name should not be null or empty.\n{Todo}", JsonSerializer.Serialize(todo));
      return Results.BadRequest(new ApiResponse<object>(false, todo, NameIsRequiredError));
    }

    var user = principal.ToUserEntity();
    var updated = await todoRepository.UpdateAsync(id, todo, user.Id);

    if (updated is { IsNull: true })
    {
      Log.Logger.Debug("Todo {id} update has failed.\n{Todo}", id, JsonSerializer.Serialize(todo));
      return Results.NotFound();
    }

    Log.Logger.Debug("Todo {id} is getting updated.\n{Todo}", id, JsonSerializer.Serialize(todo));
    return Results.Ok(new ApiResponse<Todo>(true, updated));
  }

  [Authorize]
  private static async Task<IResult> DeleteAsync(
    int id,
    ITodoRepository todoRepository,
    ClaimsPrincipal principal)
  {
    var user = principal.ToUserEntity();
    var deleted = await todoRepository.DeleteAsync(id, user.Id);

    if (deleted is { IsNull: true })
    {
      Log.Logger.Debug("Todo {id} deletion has failed.\n{Todo}", id, JsonSerializer.Serialize(deleted));
      return Results.NotFound();
    }

    Log.Logger.Debug("Todo {id} is getting deleted.\n{Todo}", id, JsonSerializer.Serialize(deleted));
    return Results.Ok(new ApiResponse<Todo>(true, deleted));
  }
}