using Identity.Service.Infrastructure.Services;
using Identity.Service.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TodoList.Service.Presentation.Endpoints;

public static class IdentityEndpoints
{
  private static readonly string[] tags = ["User"];

  private const string Prefix = "api/identity";

  public static void RegisterIdentityEndpoints(this WebApplication app)
  {
    app.MapPost($"{Prefix}/register", RegisterAsync).WithTags(tags);
    app.MapPost($"{Prefix}/login", LoginAsync).WithTags(tags);
  }

  private static async Task<IResult> RegisterAsync(
    UserRegistrationModel model,
    UserManager<IdentityUser> userManager)
  {
    var user = new IdentityUser(model.UserName)
    {
      Email = model.Email
    };
    var result = await userManager.CreateAsync(user, model.Password);

    return result.Succeeded
      ? Results.Created()
      : Results.BadRequest(result.Errors);
  }

  private static async Task<IResult> LoginAsync(
    [FromBody] LoginModel model,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    ITokenService tokenService)
  {
    var user = await userManager.FindByNameAsync(model.UserName);
    if (user == null)
    {
      return Results.Unauthorized();
    }

    var passwordCheck = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
    if (!passwordCheck.Succeeded)
    {
      return Results.Unauthorized();
    }

    var token = tokenService.GenerateToken(user);
    return Results.Ok(new LoginResponseModel(token, user.Id, user.UserName ?? ""));
  }
}