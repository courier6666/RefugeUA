
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Authentication;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Announcements.Education.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Education.Create
{
    public class CreateEducationAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,Volunteer,LocalCitizen,CommunityAdmin")]
        public static async Task<IResult> CreateEducationAnnouncementAsync(
            [FromBody] EditOrCreateEducationAnnouncementCommand command,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<EditOrCreateEducationAnnouncementCommand> validator,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            long userId = httpContextAccessor!.HttpContext!.User.GetId() ?? 0;

            var educationAnnouncement = new EducationAnnouncement()
            {
                Title = command.Title,
                Content = command.Content,
                EducationType = command.EducationType,
                InstitutionName = command.InstitutionName,
                TargetGroup = string.Join(";", command.TargetGroups),
                Fee = command.Fee,
                IsFree = command.Fee == null,
                Duration = command.Duration,
                Language = command.Language,
                AuthorId = userId,
                Address = command.Address.MapToEntity(),
                ContactInformation = command.ContactInformation.MapToEntity()
            };

            if (httpContextAccessor.HttpContext.User.IsInRole(Roles.Admin))
            {
                educationAnnouncement.Accepted = true;
            }

            if (httpContextAccessor.HttpContext.User.IsInRole(Roles.CommunityAdmin) &&
                httpContextAccessor.HttpContext.User.Claims.
                FirstOrDefault(c => c.ValueType == CustomClaimTypes.District)?.Value == educationAnnouncement.Address.District)
            {
                educationAnnouncement.Accepted = true;
            }

            dbContext.EducationAnnouncements.Add(educationAnnouncement);
            await dbContext.SaveChangesAsync();

            return Results.Ok(educationAnnouncement.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/education", CreateEducationAnnouncementAsync)
                .Produces<long>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("CreateEducationAnnouncement")
                .WithTags("EducationAnnouncements")
                .RequireAuthorization();
        }
    }
}
