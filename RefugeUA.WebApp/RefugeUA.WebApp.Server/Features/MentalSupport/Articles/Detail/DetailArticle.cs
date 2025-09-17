
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Detail
{
    public class DetailArticle : IFeatureEndpoint
    {
        public static async Task<IResult> DetailArticleAsync([FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext)
        {
            var foundArticle = await dbContext.MentalSupportArticles.AsNoTracking().
                Include(a => a.Author).
                Select(MentalSupportArticleResultMapping.Expression).
                FirstOrDefaultAsync(a => a.Id == id);

            if (foundArticle == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(foundArticle);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/mental-support/articles/{id:long}", DetailArticleAsync)
                .WithName("GetMentalSupportArticle")
                .Produces<MentalSupportArticleResult>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("MentalSupport");
        }
    }
}
