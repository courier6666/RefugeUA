
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followers.Remove
{
    public class RemoveFollower : IFeatureEndpoint
    {
        public static async Task<IResult> RemoveFollowerAsync(
            [FromRoute] long groupId,
            [FromRoute] long userId,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundGroup = await dbContext.VolunteerGroups.
                Include(g => g.Followers).
                FirstOrDefaultAsync(g => g.Id == groupId);

            if (foundGroup == null)
            {
                return Results.NotFound("Group not found!");
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                VolunteerGroupResultMapping.Func(foundGroup),
                Policies.EditDeleteVolunteerGroupPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            var follower = foundGroup.Followers.FirstOrDefault(f => f.Id == userId);
            if(follower == null)
            {
                return Results.NotFound("Follower not found!");
            }

            foundGroup.Followers.Remove(follower);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/groups/{groupId:long}/followers/{userId}", RemoveFollowerAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("RemoveFollowerVolunteerGroup").
                RequireAuthorization();
        }
    }
}
