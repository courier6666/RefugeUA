
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Open
{
    public class VolunteerEventOpen : IFeatureEndpoint
    {
        public static async Task<IResult> VolunteerEventOpenAsync([FromRoute] long id,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService,
            [FromServices] RefugeUADbContext dbContext)
        {
            var foundEvent = await dbContext.VolunteerEvents.
                Include(e => e.Organizers).
                Include(e => e.Address).
                Include(e => e.Participants).
                FirstOrDefaultAsync(e => e.Id == id);
            
            if (foundEvent == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                VolunteerEventResultMapping.BaseResultFunc(foundEvent),
                Policies.EditDeleteVolunteerEventPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            if (!foundEvent.IsClosed)
            {
                return Results.BadRequest();
            }

            foundEvent.IsClosed = false;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPatch("api/volunteer/events/{id:long}/open", VolunteerEventOpenAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("OpenVolunteerEvent")
                .WithTags("Volunteer")
                .RequireAuthorization();
        }
    }
}
