
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.SpecialistsInfos.Delete
{
    public class DeletePsychologistInformation : IFeatureEndpoint
    {
        public static async Task<IResult> DeletePsychologistInformationAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
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

            dbContext.ContactInformation.Remove(foundInfo.Contact);
            dbContext.PsychologistInformation.Remove(foundInfo);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/mental-support/psychologist-informations/{id:long}", DeletePsychologistInformationAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeletePsychologistInformation")
                .WithTags("MentalSupport")
                .RequireAuthorization();
        }
    }
}
