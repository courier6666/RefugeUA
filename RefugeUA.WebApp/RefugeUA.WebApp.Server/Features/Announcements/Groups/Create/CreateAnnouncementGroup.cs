
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.Announcements.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Create
{
    public class CreateAnnouncementGroup : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> CreateAnnouncementGroupAsync(
            [FromBody] CreateAnnouncementGroupCommand command,
            [FromServices] IValidator<CreateAnnouncementGroupCommand> validator,
            [FromServices] RefugeUADbContext dbContext)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var announcementGroup = new AnnouncementGroup()
            {
                Name = command.Name,
            };

            dbContext.AnnouncementGroups.Add(announcementGroup);
            await dbContext.SaveChangesAsync();
            return Results.Created();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/groups", CreateAnnouncementGroupAsync).
                Produces(StatusCodes.Status201Created).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Announcements").
                WithName("CreateAnnouncementGroup");
        }
    }
}
