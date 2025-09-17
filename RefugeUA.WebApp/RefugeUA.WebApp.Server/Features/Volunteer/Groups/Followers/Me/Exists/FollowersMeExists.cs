
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followers.Me.Exists
{
    public class FollowersMeExists : IFeatureEndpoint
    {
        public static async Task<IResult> FollowersMeExistsAsync(
            [FromRoute] long id,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] RefugeUADbContext dbContext)
        {
            if(!(httpContextAccessor.HttpContext?.User.IsUserAuthenticated() ?? false))
            {
                return Results.Ok(false);
            }

            var eventExists = await dbContext.VolunteerGroups.AnyAsync(e => e.Id == id);

            if (!eventExists)
            {
                return Results.NotFound();
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var res = await dbContext.VolunteerGroups. 
                Include(g => g.Followers).
                Where(g => g.Id == id).
                SelectMany(g => g.Followers).AnyAsync(u => u.Id == userId);

            return Results.Ok(res);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/{id:long}/followers/me/exists", FollowersMeExistsAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("FollowersMeExistsAsync")
                .WithTags("Volunteer");
        }
    }
}
