using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Extensions.Mapping;
using RefugeUA.WebApp.Server.Features.Announcements.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.Create
{
    public class CreateAnnouncementResponse : IFeatureEndpoint
    {
        [Authorize(Roles = "MilitaryOrFamily,Admin,CommunityAdmin")]
        public static async Task<IResult> CreateAnnouncementResponseAsync(
            [FromRoute] long id,
            [FromBody] AnnouncementResponseDto response,
            [FromServices] IValidator<AnnouncementResponseDto> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var validationResult = await validator.ValidateAsync(response);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var announcement = await dbContext.Announcements.
                Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return Results.NotFound();
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            if ((await dbContext.AnnouncementResponses.
                FirstOrDefaultAsync(r => r.AnnouncementId == id && r.UserId == userId)) != null)
            {
                return Results.Conflict("Response of such user to announcement already exists.");
            }

            var announcementResponse = new AnnouncementResponse()
            {
                AnnouncementId = id,
                UserId = userId,
                ContactInformation = response.ContactInformation.MapToEntity(),
            };

            dbContext.AnnouncementResponses.Add(announcementResponse);
            await dbContext.SaveChangesAsync();
            return Results.Created();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/announcements/{id:long}/responses", CreateAnnouncementResponseAsync)
                .Produces(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status409Conflict)
                .WithName("CreateAnnouncementResponse")
                .WithTags("Announcements")
                .RequireAuthorization();
        }
    }
}
