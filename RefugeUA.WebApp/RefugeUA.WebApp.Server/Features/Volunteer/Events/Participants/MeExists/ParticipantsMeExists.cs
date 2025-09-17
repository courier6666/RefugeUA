
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Participants.MeExists
{
    public class ParticipantsMeExists : IFeatureEndpoint
    {
        [Authorize]
        public static async Task<IResult> ParticipantsMeExistsAsync(
            [FromRoute] long id,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] RefugeUADbContext dbContext)
        {
            var eventExists = await dbContext.VolunteerEvents.AnyAsync(e => e.Id == id);

            if (!eventExists)
            {
                return Results.NotFound();
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var res = await dbContext.VolunteerEvents.
                Include(e => e.Participants).
                Where(e => e.Id == id).
                SelectMany(e => e.Participants).AnyAsync(u => u.Id == userId);

            return Results.Ok(res);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/events/{id:long}/participants/me/exists", ParticipantsMeExistsAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("ParticipantsMeExistsAsync")
                .WithTags("Volunteer");
        }
    }
}
