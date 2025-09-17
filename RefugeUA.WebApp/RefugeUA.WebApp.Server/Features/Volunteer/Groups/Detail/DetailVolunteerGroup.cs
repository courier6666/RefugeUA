
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Detail
{
    public class DetailVolunteerGroup : IFeatureEndpoint
    {
        public static async Task<IResult> DetailVolunteerGroupAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var volunteerGroup = await dbContext.VolunteerGroups.AsNoTracking().
                Include(g => g.VolunteerEvents).
                Include(g => g.Followers).
                Include(g => g.Admins).
                Select(VolunteerGroupResultMapping.Expression).
                FirstOrDefaultAsync(g => g.Id == id);

            if (volunteerGroup == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(volunteerGroup);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("/api/volunteer/groups/{id:long}", DetailVolunteerGroupAsync)
                .WithName("GetVolunteerGroup")
                .Produces<VolunteerGroupResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Volunteer");
        }
    }
}
