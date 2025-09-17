
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess.Identity;

namespace RefugeUA.WebApp.Server.Features.Authentication.ConfirmEmail
{
    public class ConfirmEmail : IFeatureEndpoint
    {
        public static async Task<IResult> ConfirmEmailAsync([FromQuery] string email,
            [FromQuery] string token,
            [FromServices] UserManager<AppUser> userManager)
        {
            token = token.Replace(" ", "+");
            var foundUser = await userManager.FindByEmailAsync(email);

            if (foundUser == null)
            {
                return Results.NotFound("User not found");
            }

            var result = await userManager.ConfirmEmailAsync(foundUser, token);

            if (!result.Succeeded)
            {
                return Results.BadRequest("Invalid token");
            }

            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/auth/confirm-email", ConfirmEmailAsync).
                WithName("ConfirmEmail").
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status400BadRequest).
                WithTags("Authentication");
        }
    }
}
