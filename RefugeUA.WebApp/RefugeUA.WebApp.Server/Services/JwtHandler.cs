using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Extensions;
using RefugeUA.WebApp.Server.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RefugeUA.WebApp.Server.Services
{
    /// <summary>
    /// Handles the creation and management of JWT tokens for authentication and authorization.
    /// </summary>
    public class JwtHandler
    {
        private readonly JwtSettings jwtSettings;

        private readonly UserManager<AppUser> userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtHandler"/> class.
        /// </summary>
        /// <param name="configuration">The configuration instance to load settings.</param>
        /// <param name="userManager">The user manager for managing user-related data.</param>
        public JwtHandler(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            ArgumentNullException.ThrowIfNull(configuration);
            ArgumentNullException.ThrowIfNull(userManager);

            // Load JWT settings from the configuration
            this.jwtSettings = configuration.GetSection("JsonWebTokenKeys").Get<JwtSettings>() ?? null!;
            this.userManager = userManager;
        }

        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The application user for whom the token is generated.</param>
        /// <returns>A task representing the asynchronous operation, with the generated JWT token as a result.</returns>
        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var signingCredentials = this.GetSigningCredentials();
            var claims = user.GetClaims(this.userManager);

            // Generate JWT token options based on claims and signing credentials
            var tokenOptions = this.GenerateTokenOptions(signingCredentials, claims.ToList());

            // Return the generated token as a string
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(tokenOptions));
        }

        /// <summary>
        /// Retrieves the signing credentials used to sign the JWT token.
        /// </summary>
        /// <returns>Signing credentials for JWT creation.</returns>
        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(this.jwtSettings.IssuerSigningKey);
            var secret = new SymmetricSecurityKey(key);

            // Use HMACSHA256 algorithm for signing
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        /// <summary>
        /// Generates the JWT token options with the specified signing credentials and claims.
        /// </summary>
        /// <param name="signingCredentials">The signing credentials for the JWT token.</param>
        /// <param name="claims">The claims to include in the token.</param>
        /// <returns>A JWT token with the specified options.</returns>
        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: this.jwtSettings.ValidIssuer,
                audience: this.jwtSettings.ValidAudience,
                claims: claims,
                signingCredentials: signingCredentials,
                expires: new DateTimeOffset(DateTime.UtcNow.AddDays(7)).DateTime);

            return tokenOptions;
        }
    }
}
