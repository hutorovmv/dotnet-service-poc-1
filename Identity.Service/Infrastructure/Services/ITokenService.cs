using Microsoft.AspNetCore.Identity;

namespace Identity.Service.Infrastructure.Services;

public interface ITokenService
{
  public string GenerateToken(IdentityUser user);
}