
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;

namespace RefugeUA.WebApp.Server.Features.Announcements.Groups.Exists
{
    public class ExistsByName : IFeatureEndpoint
    {
        public static async Task<IResult> ExistsByNameAsync(
            [FromQuery] string name,
            [FromServices] RefugeUADbContext dbContext)
        {
            return Results.Ok(await dbContext.AnnouncementGroups.AnyAsync(g => g.Name == name));
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/groups/exists", ExistsByNameAsync).
                Produces<bool>(StatusCodes.Status200OK);
        }
    }
}
