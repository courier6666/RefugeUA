
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Edit
{
    public class EditPsychologistInformation : IFeatureEndpoint
    {
        public static async Task<IResult> EditPsychologistInformationAsync(
            [FromRoute] long id,
            [FromBody] EditOrCreatePsychologistInformationCommand command,
            [FromServices] IValidator<EditOrCreatePsychologistInformationCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundInfo = await dbContext.PsychologistInformation.
                Include(p => p.Author).
                Include(p => p.Contact).
                FirstOrDefaultAsync(p => p.Id == id);

            if (foundInfo == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                PsychologistInformationResultMapping.Func(foundInfo),
                Policies.EditDeletePsychologistInformationPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            foundInfo.Title = command.Title;
            foundInfo.Description = command.Description;
            command.ContactInformation.MapToExistingEntityFull(foundInfo.Contact);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/mental-support/psychologist-informations/{id:long}", EditPsychologistInformationAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("EditPsychologistInformation")
                .WithTags("MentalSupport")
                .RequireAuthorization();
        }
    }
}
