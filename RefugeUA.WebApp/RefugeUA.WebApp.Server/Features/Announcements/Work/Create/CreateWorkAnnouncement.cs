
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Announcements.Work.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Create
{
    public class CreateWorkAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,Volunteer,LocalCitizen,CommunityAdmin")]
        public static async Task<IResult> CreateWorkAnnouncementAsync(
            [FromBody] EditOrCreateWorkAnnouncementCommand command,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<EditOrCreateWorkAnnouncementCommand> validator,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if(!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            long userId = httpContextAccessor!.HttpContext!.User.GetId() ?? 0;

            var workAnnouncement = new WorkAnnouncement
            {
                Title = command.Title,
                Content = command.Content,
                JobPosition = command.JobPosition,
                CompanyName = command.CompanyName,
                SalaryLower = command.SalaryLower,
                SalaryUpper = command.SalaryUpper,
                RequirementsContent = command.RequirementsContent,
                WorkCategoryId = command.WorkCategoryId,
                AuthorId = userId,
                Address = command.Address.MapToEntity(),
                ContactInformation = command.ContactInformation.MapToEntity()
            };

            dbContext.WorkAnnouncements.Add(workAnnouncement);
            await dbContext.SaveChangesAsync();

            return Results.Ok(workAnnouncement.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/work", CreateWorkAnnouncementAsync)
                .Produces<long>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("CreateWorkAnnouncement")
                .WithTags("WorkAnnouncements")
                .RequireAuthorization();
        }
    }
}
