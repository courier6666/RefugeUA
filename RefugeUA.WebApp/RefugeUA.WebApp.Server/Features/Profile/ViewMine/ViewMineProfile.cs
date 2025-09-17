
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.User;

namespace RefugeUA.WebApp.Server.Features.Profile.ViewMine
{
    public class ViewMineProfile : IFeatureEndpoint
    {
        public static async Task<IResult> ViewMineProfileAsync(
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var user = await dbContext.Users.
                Include(u => u.UserRoles).
                ThenInclude(ur => ur.Role).
                Select(UserDtoWithIdAdminMappingExpression.AppUserToDtoWithIdAdmin).
                FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/profile/mine", ViewMineProfileAsync).
                Produces<UserDtoWithId>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status401Unauthorized).
                RequireAuthorization();
        }
    }
}
