
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Features.Authentication.AcceptUser
{
    public class AcceptUser : IFeatureEndpoint
    {
        public static async Task<IResult> AcceptUserAsync(
            [FromRoute] long id,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundUser = await userManager.Users
                .FirstOrDefaultAsync(x => x.Id == id);

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

            foundUser.IsAccepted = true;
            var result = await userManager.UpdateAsync(foundUser);
            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .ToDictionary(e => e.Code, e => new List<string>() { e.Description }) as IDictionary<string, string[]>;
                return Results.ValidationProblem(errors!);
            }

            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/auth/users/{id:long}/accept", AcceptUserAsync)
                .WithName("AcceptUser")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithTags("Authentication")
                .RequireAuthorization();
        }
    }
}
