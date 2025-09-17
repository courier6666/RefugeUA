
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Edit
{
    public class EditVolunteerGroup : IFeatureEndpoint
    {
        public static async Task<IResult> EditVolunteerGroupAsync(
            [FromRoute] long id,
            [FromBody] EditOrCreateVolunteerGroupCommand command,
            [FromServices] IValidator<EditOrCreateVolunteerGroupCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

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

            foundGroup.Title = command.Title;
            foundGroup.DescriptionContent = command.DescriptionContent;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/volunteer/groups/{id:long}", EditVolunteerGroupAsync).
                Produces(StatusCodes.Status201Created).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("EditVolunteerGroup").
                RequireAuthorization();
        }
    }
}
