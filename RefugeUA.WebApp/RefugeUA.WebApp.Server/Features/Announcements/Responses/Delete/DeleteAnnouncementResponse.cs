
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Authentication;

namespace RefugeUA.WebApp.Server.Features.Announcements.Responses.Delete
{
    public class DeleteAnnouncementResponse : IFeatureEndpoint
    {
        public static async Task<IResult> DeleteAnnouncementResponseAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var announcement = await dbContext.Announcements.
                Include(a => a.Address).FirstOrDefaultAsync(a => a.Id == id);

            if (announcement == null)
            {
                return Results.NotFound();
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;
            var response = await dbContext.AnnouncementResponses.Include(r => r.ContactInformation).
                FirstOrDefaultAsync(r => r.AnnouncementId == id && r.UserId == userId);

            if (response == null)
            {
                return Results.Conflict("Response of such user to announcement does not exist.");
            }


            dbContext.AnnouncementResponses.Remove(response);
            dbContext.ContactInformation.Remove(response.ContactInformation);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/announcements/{id:long}/responses/mine", DeleteAnnouncementResponseAsync).
                RequireAuthorization();
        }
    }
}
