
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using static RefugeUA.WebApp.Server.Shared.Dto.User.UserDtoWithIdAdminMappingExpression;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.List
{
    public class UsersList : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin")]
        public static async Task<IResult> UsersListAsync(
            [FromQuery] string? prompt,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RoleManager<AppRole> roleManager,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var users = userManager.Users.Include(u => u.UserRoles).
                ThenInclude(ur => ur.Role).
                Where(u => u.Id != userId);

            var promptUpper = prompt?.ToUpper() ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(prompt))
            {
                users = users.Where(x => x.FirstName.ToUpper().Contains(promptUpper) || x.LastName.ToUpper().Contains(promptUpper) || x.Email.ToUpper().Contains(promptUpper));
            }

            users = users.Where(u => !u.UserRoles.Any(ur => ur.Role.Name == Roles.Admin));

            var foundUsers = await users.Select(AppUserToDtoWithIdAdmin).ToListAsync();

            if (foundUsers.Count == 0)
            {
                return Results.NotFound();
            }

            return Results.Ok(foundUsers);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/admin/users", UsersListAsync)
                .Produces<List<AppUser>>()
                .Produces(StatusCodes.Status404NotFound)
                .WithName("UsersList")
                .WithTags("Admin")
                .RequireAuthorization();
        }
    }
}
