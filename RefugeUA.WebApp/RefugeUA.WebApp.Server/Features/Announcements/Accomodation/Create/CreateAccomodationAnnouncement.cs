using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.FileManager;
using RefugeUA.WebApp.Server.Authentication;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Common;

namespace RefugeUA.WebApp.Server.Features.Announcements.Accomodation.Create
{
    public class CreateAccomodationAnnouncement : IFeatureEndpoint
    {
        [Authorize(Roles = "Admin,Volunteer,LocalCitizen,CommunityAdmin")]
        [Consumes("multipart/form-data")]
        public static async Task<IResult> CreateAccomodationAnnouncementAsync(
            [FromForm] EditOrCreateAccomodationAnnouncementCommand command,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IValidator<EditOrCreateAccomodationAnnouncementCommand> validator,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IFileManager fileManager,
            HttpRequest request)
        {
            var form = await request.ReadFormAsync();
            if (form.Files != null && form.Files.Count > 0)
            {
                command.Images = form.Files.ToList();
            }

            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            long userId = httpContextAccessor!.HttpContext!.User.GetId() ?? 0;

            var accomodationAnnouncement = new AccomodationAnnouncement()
            {
                Title = command.Title,
                Content = command.Content,
                BuildingType = command.BuildingType,
                NumberOfRooms = command.NumberOfRooms,
                Floors = command.Floors,
                PetsAllowed = command.PetsAllowed,
                Capacity = command.Capacity,
                AreaSqMeters = command.AreaSqMeters,
                Price = command.Price,
                IsFree = command.Price == null,
                AuthorId = userId,
                Address = command.Address.MapToEntity(),
                ContactInformation = command.ContactInformation.MapToEntity(),
            };

            using var ms = new MemoryStream();
            if (command.Images != null && command.Images.Count > 0)
            {
                var createdImages = command.Images.Select(async i =>
                {
                    await i.CopyToAsync(ms);
                    var filename = await fileManager.UploadFileAsync(ms.ToArray(), "images", ".jpg");

                    ms.SetLength(0);
                    ms.Position = 0;
                    
                    return new Image()
                    {
                        Path = filename!,
                    };
                }).Select(t => t.Result).ToList();

                accomodationAnnouncement.Images = createdImages;
            }

            if (httpContextAccessor.HttpContext.User.IsInRole(Roles.Admin))
            {
                accomodationAnnouncement.Accepted = true;
            }

            if (httpContextAccessor.HttpContext.User.IsInRole(Roles.CommunityAdmin) &&
                httpContextAccessor.HttpContext.User.Claims.
                FirstOrDefault(c => c.ValueType == CustomClaimTypes.District)?.Value == accomodationAnnouncement.Address.District)
            {
                accomodationAnnouncement.Accepted = true;
            }

            dbContext.AccomodationAnnouncements.Add(accomodationAnnouncement);
            await dbContext.SaveChangesAsync();

            return Results.Ok(accomodationAnnouncement.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/accomodation", CreateAccomodationAnnouncementAsync)
                .DisableAntiforgery()
                .Produces<long>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("CreateAccomodationAnnouncement")
                .WithTags("AccomodationAnnouncements")
                .RequireAuthorization();
        }
    }
}
