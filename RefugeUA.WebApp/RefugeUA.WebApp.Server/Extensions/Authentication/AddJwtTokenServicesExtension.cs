using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RefugeUA.WebApp.Server.Options;

namespace RefugeUA.WebApp.Server.Extensions.Authentication
{
    public static class AddJwtTokenServicesExtension
    {
        /// <summary>
        /// Adds JWT token services to the service collection.
        /// This method configures the authentication middleware to use JWT Bearer tokens for authentication.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to which the services should be added.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> containing the JWT settings.</param>
        public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add Jwt Settings
            var bindJwtSettings = new JwtSettings();
            configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            _ = services.AddSingleton(bindJwtSettings);

            _ = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies["jwt"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        else
                        {
                            context.Token = null;
                        }

                        return Task.CompletedTask;
                    },
                };
            });
        }
    }
}
