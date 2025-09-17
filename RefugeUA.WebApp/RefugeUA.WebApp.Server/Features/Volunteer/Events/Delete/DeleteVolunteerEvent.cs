
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Shared.Dto.BaseAnnouncement;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Delete
{
    public class DeleteVolunteerEvent : IFeatureEndpoint
    {
        public static async Task<IResult> DeleteVolunteerEventAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundEvent = await dbContext.VolunteerEvents
                .Include(e => e.Organizers)
                .Include(e => e.Address)
                .Include(e => e.VolunteerGroup)
                .FirstOrDefaultAsync(e => e.Id == id);

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

            dbContext.VolunteerEvents.Remove(foundEvent);
            if (foundEvent.Address != null)
            {
                foundEvent.AddressId = null;
                dbContext.Addresses.Remove(foundEvent.Address);
            }
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/events/{id:long}", DeleteVolunteerEventAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeleteVolunteerEvent")
                .WithTags("Volunteer")
                .RequireAuthorization();
        }
    }
}
