
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Leave
{
    public class LeaveGroup : IFeatureEndpoint
    {
        public static async Task<IResult> LeaveGroupAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var foundGroup = await dbContext.VolunteerGroups.
                Include(g => g.Followers).
                Include(g => g.Admins).
                FirstOrDefaultAsync(g => g.Id == id);

            if (foundUser == null)
            {
                return Results.Unauthorized();
            }

            if (foundGroup == null)
            {
                return Results.NotFound();
            }

            if (foundGroup.Admins.Any(o => o.Id == userId))
            {
                return Results.BadRequest("You are an admin.");
            }

            if (!foundGroup.Followers.Any(p => p.Id == userId))
            {
                return Results.BadRequest("You are already not following the group.");
            }

            foundGroup.Followers.Remove(foundUser);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/groups/{id:long}/leave", LeaveGroupAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status404NotFound).
                WithTags("Volunteer").
                WithName("LeaveVolunteerGroup").
                RequireAuthorization();
        }
    }
}
