
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Delete
{
    public class DeleteArticle : IFeatureEndpoint
    {
        public static async Task<IResult> DeleteArticleAsync(
            [FromRoute] long id,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var foundArticle = await dbContext.MentalSupportArticles.
                Include(a => a.Author).
                FirstOrDefaultAsync(a => a.Id == id);

            if (foundArticle == null)
            {
                return Results.NotFound();
            }

            if (!(await authService.AuthorizeAsync(
                httpContextAccessor.HttpContext!.User,
                MentalSupportArticleResultMapping.Func(foundArticle),
                Policies.EditDeleteMentalSupportArticlePolicy)).Succeeded)
            {
                return Results.Forbid();
            }

            dbContext.MentalSupportArticles.Remove(foundArticle);
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("api/mental-support/articles/{id:long}", DeleteArticleAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("DeleteArticle")
                .WithTags("MentalSupport")
                .RequireAuthorization();
        }
    }
}
