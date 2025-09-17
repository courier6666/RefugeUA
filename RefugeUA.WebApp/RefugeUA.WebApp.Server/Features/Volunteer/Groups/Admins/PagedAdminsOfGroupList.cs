
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using RefugeUA.Entities.Interfaces;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Admins
{
    public class PagedAdminsOfGroupList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedAdminsOfGroupListAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [AsParameters] PagedAdminsOfGroupListQuery query,
            [FromServices] IValidator<PagedAdminsOfGroupListQuery> validator)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundAdmins = dbContext.VolunteerGroups.Where(e => e.Id == id)
                .SelectMany(e => e.Admins);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                foundAdmins = foundAdmins.
                    Where(p => (p.FirstName.ToUpper() + " " + p.LastName).Contains(promptUpper) ||
                        p.Email.ToUpper().Contains(promptUpper));
            }

            var totalCount = await foundAdmins.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No admins found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedAdmins = await foundAdmins.Select(u => u as AppUser).
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


            foreach (var user in pagedAdmins)
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

            var pagingInfo = new PagingInfo<UserDtoWithId>(pagedAdmins, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/{id:long}/admins", PagedAdminsOfGroupListAsync).
                Produces<PagingInfo<UserDtoWithId>>().
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                RequireAuthorization();
        }
    }
}
