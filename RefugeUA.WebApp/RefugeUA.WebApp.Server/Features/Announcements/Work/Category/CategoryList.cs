using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Shared.Dto.WorkCategory;

namespace RefugeUA.WebApp.Server.Features.Announcements.Work.Category
{
    public class CategoryList : IFeatureEndpoint
    {
        public static async Task<IResult> CategoryListAsync([FromServices]RefugeUADbContext dbContext)
        {
            var categories = await dbContext.WorkCategories.
                AsNoTracking().
                Select(c => new WorkCategoryDtoWithId()
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToListAsync();

            return Results.Ok(categories);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/announcements/work/categories", CategoryListAsync)
                .Produces(StatusCodes.Status200OK)
                .Produces<IEnumerable<WorkCategoryDtoWithId>>()
                .WithName("Categories")
                .WithTags("WorkAnnouncements");
        }
    }
}
