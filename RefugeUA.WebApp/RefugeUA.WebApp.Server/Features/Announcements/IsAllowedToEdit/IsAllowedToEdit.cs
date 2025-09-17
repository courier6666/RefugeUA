
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.IsAllowedToEdit
{
    public class IsAllowedToEdit : IFeatureEndpoint
    {
        public static async Task<IResult> IsAllowedToEditAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundAnnouncement = await dbContext.Announcements
                .Include(x => x.Address)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }
            
            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                BaseAnnouncementResultMapping.Func(foundAnnouncement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Ok(false);
            }

            return Results.Ok(true);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/{id:long}/is-allowed-to-edit", IsAllowedToEditAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("IsAllowedToEditAnnouncement")
                .WithTags("Announcements");
        }
    }
}
