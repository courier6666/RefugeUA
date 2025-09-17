
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Shared.Dto.User;
using Microsoft.EntityFrameworkCore;

namespace RefugeUA.WebApp.Server.Features.Profile.ViewOther
{
    public class ViewOtherProfile : IFeatureEndpoint
    {
        public static async Task<IResult> ViewOtherProfileAsync(
            [FromRoute] long id,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var user = await dbContext.Users.
                Include(u => u.UserRoles).
                ThenInclude(ur => ur.Role).
                Select(UserDtoWithIdAdminMappingExpression.AppUserToDtoWithIdAdmin).
                FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(user);
        }
        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/users/{id:long}/profile/", ViewOtherProfileAsync).
                Produces<UserDtoWithId>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
