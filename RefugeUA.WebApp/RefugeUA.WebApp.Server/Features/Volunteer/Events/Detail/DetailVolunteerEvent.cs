
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Detail
{
    public class DetailVolunteerEvent : IFeatureEndpoint
    {
        public static async Task<IResult> DetailVolunteerEventAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var foundEvent = await dbContext.VolunteerEvents.AsNoTracking().
                Include(e => e.Organizers).
                Include(e => e.Address).
                Include(e => e.ScheduleItems).
                Include(e => e.VolunteerGroup).
                Include(e => e.Participants).
                Select(VolunteerEventResultMapping.DetailResultExpression).
                FirstOrDefaultAsync(e => e.Id == id);

            if(foundEvent == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(foundEvent);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/events/{id:long}", DetailVolunteerEventAsync)
                .WithName("GetVolunteerEvent")
                .Produces<VolunteerEventDetailResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Volunteer");
        }
    }
}
