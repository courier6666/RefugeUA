using Microsoft.AspNetCore.Identity;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authentication;
using System.Globalization;
using System.Security.Claims;

namespace RefugeUA.WebApp.Server.Extensions
{
    public static class AppUserClaimsExtension
    {

        /// <summary>
        /// Generates a collection of claims for the specified <see cref="AppUser"/> using the provided <see cref="UserManager{AppUser}"/>.
        /// </summary>
        /// <param name="user">The <see cref="AppUser"/> object for which claims are to be generated.</param>
        /// <param name="userManager">The <see cref="UserManager{AppUser}"/> instance used to retrieve the user's roles.</param>
        /// <returns>A collection of <see cref="Claim"/> objects representing the user's claims.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="user"/> or <paramref name="userManager"/> is <c>null</c>.</exception>
        public static ICollection<Claim> GetClaims(this AppUser user, UserManager<AppUser> userManager)
        {

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(CustomClaimTypes.District, user.District ?? string.Empty), 
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
            };

            foreach (var claim in userManager.GetRolesAsync(user).Result.Select(r => new Claim(ClaimTypes.Role, r)))
            {
                claims.Add(claim);
            }

            return claims;
        }
    }
}
