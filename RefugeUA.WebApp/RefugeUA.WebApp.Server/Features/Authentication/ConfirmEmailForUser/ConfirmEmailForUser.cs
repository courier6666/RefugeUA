
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Features.Authentication.ConfirmEmailForUser
{
    public class ConfirmEmailForUser : IFeatureEndpoint
    {
        public static async Task<IResult> ConfirmEmailForUserAsync(
            [FromRoute] long userId,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundUser = await userManager.Users
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (foundUser == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundUser,
                Policies.EditUserPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            foundUser.EmailConfirmed = true;
            await userManager.UpdateAsync(foundUser);
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("/api/auth/users/{userId:long}/confirm-email-for-user", ConfirmEmailForUserAsync).
                WithName("ConfirmEmailForUser").
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status403Forbidden).
                Produces(StatusCodes.Status401Unauthorized).
                RequireAuthorization().
                WithTags("Authentication");
        }
    }
}
