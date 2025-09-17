
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Announcements.Detail
{
    public class AnnouncementDetail : IFeatureEndpoint
    {
        public static async Task<IResult> AnnouncementDetailAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var foundAnnouncement = await dbContext.Announcements.AsNoTracking()
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Author)
                .Include(x => x.Groups)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound("Announcement not found!");
            }

            var result = BaseAnnouncementResultWithTypeMapping.AnnouncementToAnnouncementDtoModerationMapping(foundAnnouncement);
            return Results.Ok(result);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/announcements/{id:long}", AnnouncementDetailAsync)
                .WithName("GetAnnouncement")
                .Produces<BaseAnnouncementResultWithType>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Announcements");
        }
    }
}
