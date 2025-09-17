
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.Mine.Exists
{
    public class MyResponseExists : IFeatureEndpoint
    {
        public static async Task<IResult> MyResponseExistsAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor?.HttpContext?.User.GetId() ?? 0;

            var foundAnnouncement = await dbContext.
                Announcements.
                FirstOrDefaultAsync(a => a.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound(id);
            }

            return Results.Ok((await dbContext.AnnouncementResponses.
                FirstOrDefaultAsync(r => r.AnnouncementId == id && r.UserId == userId)) != null);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/{id:long}/responses/mine/exists", MyResponseExistsAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status401Unauthorized)
                .RequireAuthorization();
        }
    }
}
