using System.Security.Claims;

using CleanArchitectureSample.Application.Common.Interfaces;

namespace CleanArchitectureSample.Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
}
