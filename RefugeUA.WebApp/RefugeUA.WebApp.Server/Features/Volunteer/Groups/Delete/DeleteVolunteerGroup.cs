
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Delete
{
    public class DeleteVolunteerGroup : IFeatureEndpoint
    {
        public static async Task<IResult> DeleteVolunteerGroupAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundGroup = await dbContext.VolunteerGroups
                .FirstOrDefaultAsync(e => e.Id == id);

            if (foundGroup == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                VolunteerGroupResultMapping.Func(foundGroup),
                Policies.EditDeleteVolunteerGroupPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            dbContext.VolunteerGroups.Remove(foundGroup);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/volunteer/groups/{id:long}", DeleteVolunteerGroupAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeleteVolunteerGroup")
                .WithTags("Volunteer")
                .RequireAuthorization();
        }
    }
}
