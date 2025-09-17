
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Volunteer.Groups.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Create
{
    public class CreateVolunteerGroup : IFeatureEndpoint
    {
        [Authorize(Roles = "CommunityAdmin,Admin,Volunteer")]
        public static async Task<IResult> CreateVolunteerGroupAsync(
            [FromBody] EditOrCreateVolunteerGroupCommand command,
            [FromServices] IValidator<EditOrCreateVolunteerGroupCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var foundUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var volunteerGroup = new VolunteerGroup()
            {
                Title = command.Title,
                DescriptionContent = command.DescriptionContent,
                Admins = new List<IUser>() { foundUser! }
            };

            dbContext.VolunteerGroups.Add(volunteerGroup);
            await dbContext.SaveChangesAsync();

            return Results.Ok(volunteerGroup.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/volunteer/groups", CreateVolunteerGroupAsync).
                Produces<long>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status201Created).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("CreateGroup").
                RequireAuthorization();
        }
    }
}
