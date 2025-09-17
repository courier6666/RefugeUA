
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.FileManager;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Edit
{
    public class EditAccomodationAnnouncement : IFeatureEndpoint
    {
        [Consumes("multipart/form-data")]
        public static async Task<IResult> EditAccomodationAnnouncementAsync([FromRoute] long id,
            [FromForm] EditOrCreateAccomodationAnnouncementCommand command,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService,
            [FromServices] IValidator<EditOrCreateAccomodationAnnouncementCommand> validator,
            [FromServices] IFileManager fileManager,
            HttpRequest request)
        {
            var form = await request.ReadFormAsync();
            if (form.Files != null && form.Files.Count > 0)
            {
                command.Images = form.Files.ToList();
            }

            var foundAnnouncement = await dbContext.AccomodationAnnouncements
                .Include(x => x.Address)
                .Include(x => x.ContactInformation)
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (foundAnnouncement == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                AccomodationAnnouncementMapping.Func(foundAnnouncement),
                Policies.EditDeleteAnnouncementPolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            foundAnnouncement.Title = command.Title;
            foundAnnouncement.Content = command.Content;
            foundAnnouncement.BuildingType = command.BuildingType;
            foundAnnouncement.NumberOfRooms = command.NumberOfRooms;
            foundAnnouncement.Floors = command.Floors;
            foundAnnouncement.PetsAllowed = command.PetsAllowed;
            foundAnnouncement.Capacity = command.Capacity;
            foundAnnouncement.AreaSqMeters = command.AreaSqMeters;
            foundAnnouncement.Price = command.Price;
            foundAnnouncement.IsFree = command.Price == null;
            foundAnnouncement.NonAcceptenceReason = null;
            command.Address.MapToExistingEntityFull(foundAnnouncement.Address);
            command.ContactInformation.MapToExistingEntityFull(foundAnnouncement.ContactInformation);

            var imagesNames = command.Images?.Select(i => i.FileName) ?? [];
            IEnumerable<Image> imagesToRemove = foundAnnouncement.Images?.Where(i => !imagesNames.Contains(i.Path)) ?? [];

            foreach (var image in imagesToRemove)
            {
                foundAnnouncement.Images?.Remove(image);
                await fileManager.RemoveFileAsync("images", image.Path);
            }

            var imagesToAdd = command.Images?.Where(i => !foundAnnouncement.Images?.Select(im => im.Path).Contains(i.FileName) ?? true) ?? [];

            using var ms = new MemoryStream();

            if(foundAnnouncement.Images == null)
            {
                foundAnnouncement.Images = new List<Image>();
            }

            foreach (var image in imagesToAdd)
            {
                await image.CopyToAsync(ms);
                var filename = await fileManager.UploadFileAsync(ms.ToArray(), "images", "jpg");
                Image img = new Image()
                {
                    Path = filename!,
                };

                foundAnnouncement.Images.Add(img);

                ms.SetLength(0);
                ms.Position = 0;
            }

            dbContext.Update(foundAnnouncement);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/announcements/accomodation/{id:long}", EditAccomodationAnnouncementAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("EditAccomodationAnnouncement")
                .DisableAntiforgery()
                .WithTags("AccomodationAnnouncements")
                .RequireAuthorization();
        }
    }
}
