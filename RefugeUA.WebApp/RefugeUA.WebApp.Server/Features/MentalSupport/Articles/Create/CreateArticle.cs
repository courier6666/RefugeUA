
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Extensions.Authentication;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Create
{
    public class CreateArticle : IFeatureEndpoint
    {
        [Authorize(Roles = "CommunityAdmin,Admin")]
        public static async Task<IResult> CreateMentalSupportArticleAsync(
            [FromBody] EditOrCreateMentalSupportArticleCommand command,
            [FromServices] IValidator<EditOrCreateMentalSupportArticleCommand> validator,
            [FromServices] RefugeUADbContext dbContext,
            [FromServices] IHttpContextAccessor httpContextAccessor)
        {
            var validationResult = await validator.ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var userId = httpContextAccessor.HttpContext?.User.GetId() ?? 0;

            var article = new MentalSupportArticle()
            {
                AuthorId = userId,
                Title = command.Title,
                Content = command.Content,
            };

            dbContext.MentalSupportArticles.Add(article);
            await dbContext.SaveChangesAsync();
            return Results.Ok(article.Id);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/mental-support/articles", CreateMentalSupportArticleAsync).
                Produces<long>(StatusCodes.Status200OK).
                Produces(StatusCodes.Status400BadRequest).
                Produces(StatusCodes.Status401Unauthorized).
                Produces(StatusCodes.Status403Forbidden).
                WithTags("MentalSupport").
                WithName("CreateArticle").
                RequireAuthorization();
        }
    }
}