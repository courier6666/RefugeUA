
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Participants.Remove
{
    public class RemoveParticipant : IFeatureEndpoint
    {
        public static async Task<IResult> RemoveParticipantAsync(
            [FromRoute] long eventId,
            [FromRoute] long userId,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundEvent = await dbContext.VolunteerEvents.
                Include(e => e.Participants).
                Include(e => e.Organizers).
                Include(e => e.Address).
                FirstOrDefaultAsync(e => e.Id == eventId);

            if (foundEvent == null)
            {
                return Results.NotFound("Event not found!");
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                VolunteerEventResultMapping.BaseResultFunc(foundEvent),
                Policies.EditDeleteVolunteerEventPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            var participant = foundEvent.Participants.FirstOrDefault(f => f.Id == userId);
            if (participant == null)
            {
                return Results.NotFound("Participant not found!");
            }

            foundEvent.Participants.Remove(participant);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/events/{eventId:long}/participants/{userId}", RemoveParticipantAsync).
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("RemoveParticipantVolunteerEvent").
                RequireAuthorization();
        }
    }
}
