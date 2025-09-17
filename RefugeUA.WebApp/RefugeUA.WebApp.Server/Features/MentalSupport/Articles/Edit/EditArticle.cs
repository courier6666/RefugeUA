
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.WebApp.Server.Authorization.Constants;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Edit
{
    public class EditArticle : IFeatureEndpoint
    {
        public static async Task<IResult> EditArticleAsync(
            [FromRoute] long id,
            [AsParameters] EditOrCreateMentalSupportArticleCommand command,
            [FromServices] IValidator<EditOrCreateMentalSupportArticleCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor,
            [FromServices] IAuthorizationService authService)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

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

            foundArticle.Title = command.Title;
            foundArticle.Content = command.Content;
            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("api/mental-support/articles/{id:long}", EditArticleAsync)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status403Forbidden)
                .Produces(StatusCodes.Status401Unauthorized)
                .WithName("EditArticle")
                .WithTags("MentalSupport")
                .RequireAuthorization();
        }
    }
}
