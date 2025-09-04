using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManager.Application.Contracts;

namespace TaskManager.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public CurrentUserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string UserId =>
            _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}