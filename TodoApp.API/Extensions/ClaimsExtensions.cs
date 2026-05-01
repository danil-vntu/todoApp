using System.Security.Claims;

namespace TodoApp.API.Extensions
{
    public static class ClaimsExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim not found in token.");
            }

            if (!int.TryParse(userIdClaim.Value, out var userId))
            {
                throw new InvalidOperationException("User ID claim is not a valid integer.");
            }

            return userId;
        }
    }
}