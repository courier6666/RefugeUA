
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess.Identity;

namespace RefugeUA.WebApp.Server.Features.Authentication.EmailExists
{
    public class EmailExists : IFeatureEndpoint
    {
        public static async Task<IResult> EmailExistsAsync(
            [FromQuery] string email,
            [FromServices] UserManager<AppUser> userManager)
        {
            var emailExists = await userManager.FindByEmailAsync(email) != null;

            if (emailExists)
            {
                return Results.Ok(true);
            }

            return Results.Ok(false);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/auth/email-exists", EmailExistsAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces<bool>()
                .WithName("EmailExists")
                .WithTags("Authentication");
        }
    }
}
