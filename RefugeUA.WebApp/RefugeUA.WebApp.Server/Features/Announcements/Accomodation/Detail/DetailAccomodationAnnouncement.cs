using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Detail
{
    public class DetailAccomodationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> DetailAccomodationAnnouncementAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundAccomodationAnnouncement = await dbContext.AccomodationAnnouncements
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Author)
                .Include(x => x.Groups)
                .Select(DetailAccomodationAnnouncementMapping.Expression)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAccomodationAnnouncement == null)
            {
                return Results.NotFound();
            }

            // acceptence details are only available for admins, moderators and announcement author
            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundAccomodationAnnouncement,
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                foundAccomodationAnnouncement.NonAcceptenceReason = null;
                foundAccomodationAnnouncement.IsAccepted = null;
            }

            return Results.Ok(foundAccomodationAnnouncement);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/announcements/accomodation/{id:long}", DetailAccomodationAnnouncementAsync)
                .WithName("GetAccomodationAnnouncement")
                .Produces<AccomodationAnnouncementResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("AccomodationAnnouncements");
        }
    }
}
