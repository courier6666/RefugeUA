
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefugeUA.DatabaseAccess;
using RefugeUA.Entities;
using RefugeUA.WebApp.Server.Features.MentalSupport.Articles.Common;
using RefugeUA.WebApp.Server.Features.Volunteer.Events.Common;
using RefugeUA.WebApp.Server.Shared.Dto.PagingInfo;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RefugeUA.WebApp.Server.Features.MentalSupport.Articles.PagedList
{
    public class PagedArticlesList : IFeatureEndpoint
    {
        public static async Task<IResult> PagedArticlesListAsync(
            [AsParameters] PagedArticlesListQuery query,
            [FromServices] IValidator<PagedArticlesListQuery> validator,
            [FromServices] RefugeUADbContext dbContext)
        {
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var articles = dbContext.MentalSupportArticles.AsNoTracking().
                Include(a => a.Author).
                AsQueryable();

            if (query.Prompt != null)
            {
                var queryPrompt = query.Prompt.ToUpper();
                articles = articles.Where(a => a.Title.ToUpper().Contains(queryPrompt));
            }

            var totalCount = await articles.CountAsync();

            if (totalCount == 0)
            {
                return Results.NotFound("No articles found with such parameters!");
            }

            var totalPages = (int)Math.Ceiling((double)totalCount / query.PageLength);
            var page = Math.Max(1, Math.Min(query.Page, totalPages));

            var pagedArticles = await articles.
                OrderByDescending(a => a.CreatedAt).
                Select(MentalSupportArticleResultMapping.Expression).
                Skip((page - 1) * query.PageLength).
                Take(query.PageLength).
                ToListAsync();

            var pagingInfo = new PagingInfo<MentalSupportArticleResult>(pagedArticles, totalCount, page, query.PageLength);

            return Results.Ok(pagingInfo);
        }

        public void AddEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/mental-support/articles/paged", PagedArticlesListAsync)
                .Produces<PagingInfo<MentalSupportArticleResult>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("PagedListArticles")
                .WithTags("MentalSUpport");
        }
    }
}
