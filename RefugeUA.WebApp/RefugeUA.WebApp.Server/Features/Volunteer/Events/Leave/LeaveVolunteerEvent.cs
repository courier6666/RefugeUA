
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Leave
{
    public class LeaveVolunteerEvent : IFeatureEndpoint
    {
        public static async Task<IResult> LeaveVolunteerEventAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var foundEvent = await dbContext.VolunteerEvents
                .Include(e => e.Participants)
                .Include(e => e.Organizers)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (foundUser == null)
            {
                return Results.Unauthorized();
            }

            if (foundEvent == null)
            {
                return Results.NotFound();
            }

            if (foundEvent.Organizers.Any(o => o.Id == userId))
            {
                return Results.BadRequest("You cannot leave event you are organizing.");
            }

            if (!foundEvent.Participants.Any(p => p.Id == userId))
            {
                return Results.BadRequest("You are already not participating for this event.");
            }

            foundEvent.Participants.Remove(foundUser);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/events/{id:long}/leave", LeaveVolunteerEventAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status404NotFound).
                WithTags("Volunteer").
                WithName("LeaveVolunteerEvent").
                RequireAuthorization();
        }
    }
}
