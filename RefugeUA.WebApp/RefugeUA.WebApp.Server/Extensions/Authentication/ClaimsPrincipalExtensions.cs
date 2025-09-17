using System.Security.Claims;

namespace RefugeUA.WebApp.Server.Extensions.Authentication
{
    /// <summary>
    /// Provides extension methods for working with <see cref="ClaimsPrincipal"/> objects in the TodoList application.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retrieves the user ID from the claims of the given <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> object representing the current user.</param>
        /// <returns>The user ID as a nullable <see cref="long"/> if present in the claims, otherwise <c>null</c>.</returns>
        public static long? GetId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return null;
            }

            _ = long.TryParse(userId, out var userIdClaim);

            return userIdClaim;
        }

        public static string? GetDistrict(this ClaimsPrincipal? principal)
        {
            string? district = principal?.FindFirstValue("District");
            if (district == null)
            {
                return null;
            }
            return district;
        }

        /// <summary>
        /// Determines if the given <see cref="ClaimsPrincipal"/> represents an authenticated user.
        /// </summary>
        /// <param name="principal">The <see cref="ClaimsPrincipal"/> object representing the current user.</param>
        /// <returns><c>true</c> if the user is authenticated; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="principal"/> is <c>null</c>.</exception>
        public static bool IsUserAuthenticated(this ClaimsPrincipal principal)
        {
            ValidateNotNull(principal);

            return principal.Identity!.IsAuthenticated;
        }

        private static void ValidateNotNull<T>(T? value)
        {
            ArgumentNullException.ThrowIfNull(value);
        }
    }
}
