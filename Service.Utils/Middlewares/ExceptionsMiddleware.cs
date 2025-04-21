using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Service.Utils.Models;

namespace Service.Utils.Middlewares;

public class ExceptionsMiddleware
{
  private readonly RequestDelegate next;

  public ExceptionsMiddleware(RequestDelegate next)
  {
    this.next = next;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = StatusCodes.Status500InternalServerError;

      var response = new ApiResponse<object>(
        Success: false,
        Data: default,
        Errors: [ex.Message]
      );

      var json = JsonSerializer.Serialize(response);
      await context.Response.WriteAsync(json);
    }
  }
}
