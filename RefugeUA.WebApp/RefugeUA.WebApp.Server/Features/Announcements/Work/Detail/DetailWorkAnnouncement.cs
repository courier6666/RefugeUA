using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities.Abstracts;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Detail
{
    public class DetailWorkAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> DetailWorkAnnouncementAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundWorkAnnouncement = await dbContext.WorkAnnouncements
                .AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.WorkCategory)
                .Include(x => x.Author)
                .Include(x => x.Groups)
                .Select(DetailWorkAnnouncementMapping.Expression)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundWorkAnnouncement == null)
            {
                return Results.NotFound();
            }

            // acceptence details are only available for admins, moderators and announcement author

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundWorkAnnouncement,
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                foundWorkAnnouncement.NonAcceptenceReason = null;
                foundWorkAnnouncement.IsAccepted = null;
            }

            return Results.Ok(foundWorkAnnouncement);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/announcements/work/{id:long}", DetailWorkAnnouncementAsync)
                .WithName("GetWorkAnnouncement")
                .Produces<WorkAnnouncementResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("WorkAnnouncements");
        }
    }
}
