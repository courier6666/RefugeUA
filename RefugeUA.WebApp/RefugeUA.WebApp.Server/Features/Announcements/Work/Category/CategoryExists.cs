
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Category
{
    public class CategoryExists : IFeatureEndpoint
    {
        public static async Task<IResult> CategoryExistsAsync(
            [FromRoute] int id,
            [FromServices] RefugeUADbContext dbContext)
        {
            return Results.Ok((await dbContext.WorkCategories.FindAsync(id)) != null);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/work/categories/{id:int}/exists", CategoryExistsAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces<bool>()
                .WithName("CategoryExists")
                .WithTags("WorkAnnouncements");
        }
    }
}
