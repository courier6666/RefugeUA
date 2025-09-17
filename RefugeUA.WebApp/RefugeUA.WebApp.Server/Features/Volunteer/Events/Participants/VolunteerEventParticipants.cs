
using Azure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities.Interfaces;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Events.Participants
{
    public class VolunteerEventParticipants : IFeatureEndpoint
    {
        public static async Task<IResult> VolunteerEventParticipantsAsync(
            [FromRoute] long id,
            [AsParameters] VolunteerEventParticipantsQuery query,
            [FromServices] IValidator<VolunteerEventParticipantsQuery> validator,
            [FromServices] RefugeUADbContext dbContext)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundParticipants = dbContext.VolunteerEvents.Where(e => e.Id == id)
                .SelectMany(e => e.Participants);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                foundParticipants = foundParticipants.
                    Where(p => (p.FirstName.ToUpper() + " " + p.LastName).Contains(promptUpper) ||
                        p.Email.ToUpper().Contains(promptUpper));
            }

            var totalCount = await foundParticipants.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No participants found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedParticipants = await foundParticipants.Select(p => p as AppUser).
                OrderByDescending(a => a.CreatedAt)
                .Select(u => new UserDtoWithId()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!,
                    CreatedAt = u.CreatedAt,
                    DateOfBirth = u.DateOfBirth,
                    Role = u.UserRoles.Select(x => x.Role)
                    .FirstOrDefault()!.Name!,
                }).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            foreach (var user in pagedParticipants)
            {
                switch (user.Role)
                {
                    case Roles.Admin:
                        user.Role = "Адміністратор";
                        break;
                    case Roles.CommunityAdmin:
                        user.Role = "Адміністратор громади";
                        break;
                    case Roles.Volunteer:
                        user.Role = "Волонтер";
                        break;
                    case Roles.MilitaryOrFamily:
                        user.Role = "Військовий, член сім'ї військового";
                        break;
                    case Roles.LocalCitizen:
                        user.Role = "Місцевий житель";
                        break;
                }
            }

            var pagingInfo = new PagingInfo<UserDtoWithId>(pagedParticipants, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/events/{id:long}/participants", VolunteerEventParticipantsAsync).
                Produces<PagingInfo<UserDtoWithId>>().
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                RequireAuthorization();
        }
    }
}
