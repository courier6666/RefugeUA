
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using FluentValidation;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.DatabaseAccess.Identity;

namespace RefugeUA.WebApp.Server.Features.Volunteer.Groups.Followers
{
    public class PagedAdminsOfGroupList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedFollowersOfGroupListAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [AsParameters] PagedFollowersOfGroupListQuery query,
            [FromServices] IValidator<PagedFollowersOfGroupListQuery> validator)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var foundFollowers = dbContext.VolunteerGroups.Where(e => e.Id == id)
                .SelectMany(e => e.Followers);

            if (query.Prompt != null)
            {
                var promptUpper = query.Prompt.ToUpper();
                foundFollowers = foundFollowers.
                    Where(p => (p.FirstName.ToUpper() + " " + p.LastName).Contains(promptUpper) ||
                        p.Email.ToUpper().Contains(promptUpper));
            }
            var totalCount = await foundFollowers.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No followers found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedFollowers = await foundFollowers.Select(f => f as AppUser).
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

            foreach (var user in pagedFollowers)
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

            var pagingInfo = new PagingInfo<UserDtoWithId>(pagedFollowers, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/volunteer/groups/{id:long}/followers", PagedFollowersOfGroupListAsync).
                Produces<PagingInfo<UserDtoWithId>>().
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                RequireAuthorization();
        }
    }
}
