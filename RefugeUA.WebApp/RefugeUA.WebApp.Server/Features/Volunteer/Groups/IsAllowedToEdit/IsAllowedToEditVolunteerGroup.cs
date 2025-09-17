
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.IsAllowedToEdit
{
    public class IsAllowedToEditVolunteerGroup : IFeatureEndpoint
    {
        public static async Task<IResult> IsAllowedToEditVolunteerGroupAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
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
                return Results.Ok(false);
            }

            return Results.Ok(true);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/{id:long}/is-allowed-to-edit", IsAllowedToEditVolunteerGroupAsync)
                .Produces<bool>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("IsAllowedToEditVolunteerGroup")
                .WithTags("Volunteer");
        }
    }
}
