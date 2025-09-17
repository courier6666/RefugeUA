
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.DatabaseAccess.Identity;
using RefugeUA.FileManager;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RefugeUA.WebApp.Server.Features.Admin.Users.EditUserProfile
{
    public class EditUserProfile : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,CommunityAdmin")]
        [Consumes("multipart/form-data")]
        public static async Task<IResult> EditUserProfileAsync([FromRoute] long id,
            [FromForm] EditUserProfileCommand command,
            [FromServices] IValidator<EditUserProfileCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] UserManager<AppUser> userManager,
            [FromServices] IAuthorizationService authService,
            [FromServices] IFileManager fileManager)
        {
            var foundUser = await dbContext.Users.FindAsync(id);

            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

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

            foundUser.DateOfBirth = command.DateOfBirth;
            foundUser.FirstName = command.FirstName;
            foundUser.LastName = command.LastName;
            foundUser.PhoneNumber = command.PhoneNumber;
            foundUser.District = command.District;

            if (command.ProfilePicture == null)
            {
                if (foundUser.ProfileImageUrl != null)
                {
                    _ = await fileManager.RemoveFileAsync("images", foundUser.ProfileImageUrl);
                    foundUser.ProfileImageUrl = null;
                }
            }
            else if (foundUser.ProfileImageUrl != command.ProfilePicture.Name)
            {

                if (foundUser.ProfileImageUrl != null)
                {
                    await fileManager.RemoveFileAsync("images", foundUser.ProfileImageUrl!);
                }

                MemoryStream memoryStream = new MemoryStream();
                await command.ProfilePicture.CopyToAsync(memoryStream);

                var res = await fileManager.UploadFileAsync(memoryStream.ToArray(), "images", "jpg");

                foundUser.ProfileImageUrl = res;
            }

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/admin/users/{id:long}", EditUserProfileAsync).
                DisableAntiforgery().
                Produces(StatusCodes.Status204NoContent).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status404NotFound).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("Admin").
                WithName("EditUserProfile");
        }
    }
}
