
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.List
{
    public class AnnouncementGroupList : IFeatureEndpoint
    {
        public static async Task<IResult> AnnouncementGroupListAsync([FromServices] RefugeUADbContext dbContext)
        {
            return Results.Ok(await dbContext.AnnouncementGroups.Select(g => new AnnouncementGroupDtoWithId()
            {
                Id = g.Id,
                Name = g.Name,
            }).ToListAsync());
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/groups", AnnouncementGroupListAsync).
                Produces<List<AnnouncementGroupDtoWithId>>(StatusCodes.Status200OK).
                WithTags("Announcements").
                WithName("AnnouncementsGroupsList");
        }
    }
}
