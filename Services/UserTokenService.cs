using System.Security.Claims;
using Zadatak1.Interfaces;

namespace Zadatak1.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserTokenService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user == null || !user.Identity.IsAuthenticated)
                return null;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return null;

            if (Guid.TryParse(userIdClaim.Value, out Guid userId))
                return userId;


            if (userId == null)
                throw new UnauthorizedAccessException();

            return null;
        }
    }
}
