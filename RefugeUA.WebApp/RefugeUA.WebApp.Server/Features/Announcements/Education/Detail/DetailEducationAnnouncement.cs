
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Detail
{
    public class DetailEducationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> DetailEducationAnnouncementAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundEducationAnnouncement = await dbContext.EducationAnnouncements
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Author)
                .Include(x => x.Groups)
                .Select(DetailEducationAnnouncementMapping.Expression)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundEducationAnnouncement == null)
            {
                return Results.NotFound();
            }

            // acceptence details are only available for admins, moderators and announcement author
            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundEducationAnnouncement,
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                foundEducationAnnouncement.NonAcceptenceReason = null;
                foundEducationAnnouncement.IsAccepted = null;
            }

            return Results.Ok(foundEducationAnnouncement);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/announcements/education/{id:long}", DetailEducationAnnouncementAsync)
                .WithName("GetEducationAnnouncement")
                .Produces<EducationAnnouncementResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("EducationAnnouncements");
        }
    }
}
