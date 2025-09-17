
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.WebApp.Server.Authorization.Constants;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.Delete
{
    public class DeleteUser : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        public static async Task<IResult> DeleteUserAsync(
            [FromRoute] long id,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IAuthorizationService authService,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var foundUser = await userManager.Users
                .Include(x => x.Announcements)
                .Include(x => x.AnnouncementResponses)
                .Include(x => x.OrganizedEvents)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundUser == null)
            {
                return Results.NotFound();
            }


            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                foundUser,
                Policies.EditUserPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            foreach (var response in foundUser.AnnouncementResponses)
            {
                dbContext.AnnouncementResponses.Remove(response);
            }

            foreach (var volunteerEvent in foundUser.OrganizedEvents)
            {
                dbContext.VolunteerEvents.Remove(volunteerEvent);
            }

            foreach (var announcement in foundUser.Announcements)
            {
                dbContext.Announcements.Remove(announcement);
            }

            var result = await userManager.DeleteAsync(foundUser);

            if (!result.Succeeded)
            {
                var errors = result.Errors
                    .ToDictionary(e => e.Code, e => new List<string>() { e.Description }) as IDictionary<string, string[]>;
                return Results.ValidationProblem(errors!);
            }

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/admin/users/{id:long}", DeleteUserAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeleteUser")
                .WithTags("Admin")
                .RequireAuthorization();
        }
    }
}
