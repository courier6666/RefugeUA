
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Edit
{
    public class EditVolunteerEvent : IFeatureEndpoint
    {
        public static async Task<IResult> EditVolunteerEventAsync([FromRoute] long id,
            [FromBody] EditCreateVolunteerEventCommand command,
            [FromServices] IValidator<EditCreateVolunteerEventCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundEvent = await dbContext.VolunteerEvents
                .Include(e => e.Organizers)
                .Include(e => e.Address)
                .Include(e => e.ScheduleItems)
                .Include(e => e.VolunteerGroup)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (foundEvent == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                VolunteerEventResultMapping.BaseResultFunc(foundEvent),
                Policies.EditDeleteVolunteerEventPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            foundEvent.Title = command.Title;
            foundEvent.Content = command.Content;
            foundEvent.StartTime = command.StartTime;
            foundEvent.EndTime = command.EndTime;
            foundEvent.DonationLink = command.DonationLink;
            foundEvent.EventType = command.EventType;
            foundEvent.OnlineConferenceLink = command.OnlineConferenceLink;
            foundEvent.VolunteerGroupId = command.VolunteerGroupId;

            if (foundEvent.Address != null)
            {
                if (command.Address != null)
                {
                    command.Address.MapToExistingEntityFull(foundEvent.Address);
                }
                else
                {
                    dbContext.Addresses.Remove(foundEvent.Address);
                    foundEvent.Address = null;
                    foundEvent.AddressId = null;
                }
            }
            else if (command.Address != null)
            {
                foundEvent.Address = command.Address.MapToEntity();
            }

            if (foundEvent.ScheduleItems?.Any() == true)
            {
                dbContext.VolunteerEventScheduleItems.RemoveRange(foundEvent.ScheduleItems);
            }

            foundEvent.ScheduleItems = command.ScheduleItems?
                .Select(item => new VolunteerEventScheduleItem
                {
                    Description = item.Description,
                    StartTime = item.StartTime
                }).ToList();

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/volunteer/events/{id:long}", EditVolunteerEventAsync).
                Produces(StatusCodes.Status201Created).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Volunteer").
                WithName("EditVolunteerEvent").
                RequireAuthorization();
        }
    }
}
