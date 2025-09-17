
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Create
{
    public class CreateVolunteerEvent : IFeatureEndpoint
    {
        [Authorize(Roles = "Volunteer,LocalCitizen,Admin,CommunityAdmin")]
        public static async Task<IResult> CreateVolunteerEventAsync(
            [FromBody] EditCreateVolunteerEventCommand command,
            [FromServices] IValidator<EditCreateVolunteerEventCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            if(command.ScheduleItems != null)
            {
                command.ScheduleItems = command.ScheduleItems.OrderBy(x => x.StartTime).ToArray();
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var organizer = await dbContext.Users.FirstAsync(u => u.Id == userId);

            var volunteerEvent = new VolunteerEvent()
            {
                Title = command.Title,
                Content = command.Content,
                StartTime = command.StartTime,
                EndTime = command.EndTime,
                ScheduleItems = command.ScheduleItems?.Select(i => new VolunteerEventScheduleItem()
                {
                    Description = i.Description,
                    StartTime = i.StartTime,
                }).ToList(),
                Address = command.Address?.MapToEntity()!,
                DonationLink = command.DonationLink,
                EventType = command.EventType,
                OnlineConferenceLink = command.OnlineConferenceLink,
                VolunteerGroupId = command.VolunteerGroupId,
                Organizers = new List<IUser>() { organizer }
            };

            dbContext.VolunteerEvents.Add(volunteerEvent);
            await dbContext.SaveChangesAsync();
            return Results.Ok(volunteerEvent.Id);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/volunteer/events", CreateVolunteerEventAsync).
                Produces<long>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("CreateEvent").
                RequireAuthorization();
        }
    }
}
