
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;
using FluentValidation;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Mapping;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Edit
{
    public class EditEducationAnnouncement : IFeatureEndpoint
    {
        public static async Task<IResult> EditEducationAnnouncementAsync(
            [FromRoute] long id,
            [FromBody] EditOrCreateEducationAnnouncementCommand command,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService,
            [FromServices] IValidator<EditOrCreateEducationAnnouncementCommand> validator)
        {
            var foundAnnouncement = await dbContext.EducationAnnouncements
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                EducationAnnouncementMapping.Func(foundAnnouncement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            foundAnnouncement.Title = command.Title;
            foundAnnouncement.Content = command.Content;
            foundAnnouncement.Fee = command.Fee;
            foundAnnouncement.EducationType = command.EducationType;
            foundAnnouncement.InstitutionName = command.InstitutionName;
            foundAnnouncement.Duration = command.Duration;
            foundAnnouncement.Language = command.Language;
            foundAnnouncement.TargetGroup = string.Join(';', command.TargetGroups);
            foundAnnouncement.NonAcceptenceReason = null;
            command.Address.MapToExistingEntityFull(foundAnnouncement.Address);
            command.ContactInformation.MapToExistingEntityFull(foundAnnouncement.ContactInformation);

            dbContext.Update(foundAnnouncement);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/announcements/education/{id:long}", EditEducationAnnouncementAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("EditEducationAnnouncement")
                .WithTags("EducationAnnouncements")
                .RequireAuthorization();
        }
    }
}
