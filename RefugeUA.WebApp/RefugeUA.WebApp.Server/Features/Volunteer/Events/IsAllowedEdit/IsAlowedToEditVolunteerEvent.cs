
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.IsAllowedEdit
{
    public class IsAlowedToEditVolunteerEvent : IFeatureEndpoint
    {
        public static async Task<IResult> IsAllowedToEditVolunteerEventAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundEvent = await dbContext.VolunteerEvents.AsNoTracking().
                Include(e => e.Organizers).
                Select(VolunteerEventResultMapping.BaseResultExpression).
                FirstOrDefaultAsync(e => e.Id == id);

            if (foundEvent == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundEvent,
                Policies.EditDeleteVolunteerEventPolicy)).Succeeded)
            {
                return Results.Ok(false);
            }

            return Results.Ok(true);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/events/{id:long}/is-allowed-to-edit", IsAllowedToEditVolunteerEventAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("IsAllowedToEditVolunteerEvent")
                .WithTags("Volunteer");
        }
    }
}
