
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using static RefugeUA.WebApp.Server.Shared.Dto.User.UserDtoWithIdAdminMappingExpression;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.PagedList
{
    public class PagedUsersList : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> PagedUsersListAsync(
            [AsParameters] PagedAdminListUsersQuery query,
            [FromServices] IValidator<PagedAdminListUsersQuery> validator,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RoleManager<AppRole> roleManager,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(query);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            // making sure that admin won't view himself
            var users = dbContext.Users.Include(u => u.UserRoles).
                ThenInclude(ur => ur.Role).
                Where(u => u.Id != userId);

            var promptUpper = query.Prompt?.ToUpper() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(query.Prompt))
            {
                users = users.Where(x => x.FirstName.ToUpper().Contains(promptUpper) || x.LastName.ToUpper().Contains(promptUpper) || x.Email.ToUpper().Contains(promptUpper));
            }

            if (httpContextAccessor.HttpContext!.User.IsInRole(Roles.Admin))
            {
                users = users.Where(u => !u.UserRoles.Any(ur => ur.Role.Name == Roles.Admin));
            }
            else if(httpContextAccessor.HttpContext!.User.IsInRole(Roles.CommunityAdmin))
            {
                var district = httpContextAccessor.HttpContext.User.GetDistrict();
                users = users.Where(u => !u.UserRoles.Any(ur => ur.Role.Name == Roles.Admin || ur.Role.Name == Roles.CommunityAdmin) &&
                    u.District == district);
            }

            var totalCount = await users.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No users found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedUsers = await users.
                OrderByDescending(a => a.CreatedAt).
                Select(AppUserToDtoWithIdAdmin).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            foreach (var user in pagedUsers)
            {
                switch(user.Role)
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

            var pagingInfo = new PagingInfo<UserDtoWithIdAdmin>(pagedUsers, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }
        
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/admin/users/paged", PagedUsersListAsync)
                .Produces<PagingInfo<AppUser>>()
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PagedUsersList")
                .WithTags("Admin")
                .RequireAuthorization();
        }
    }
}
