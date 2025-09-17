
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.IsAnAdminOfGroup
{
    public class IsAdminOfGroup : IFeatureEndpoint
    {
        public static async Task<IResult> IsAdminOfGroupAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            if (!(httpContextAccessor.HttpContext?.User.IsUserAuthenticated() ?? false))
            {
                return Results.Ok(false);
            }

            var userId = httpContextAccessor.HttpContext!.User.GetId() ?? 0;

            var groupExists = await dbContext.VolunteerGroups.AnyAsync(g => g.Id == id);

            if (!groupExists)
            {
                return Results.NotFound();
            }
            var isAdmin = await dbContext.VolunteerGroups.
                Include(g => g.Admins).
                Where(g => g.Id == id).
                SelectMany(g => g.Admins).
                AnyAsync(a => a.Id == userId);

            return Results.Ok(isAdmin);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/{id:long}/am-admin-of-group", IsAdminOfGroupAsync).
                Produces<bool>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status404NotFound).
                WithName("IsMeAnAmindOfGroup").
                WithTags("Volunteer");
        }
    }
}
